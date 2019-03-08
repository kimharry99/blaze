using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct infoFurniture
{
    public int level;
    public bool isRun;

    public infoFurniture(int Level, bool IsRun)
    {
        level = Level;
        isRun = IsRun;
    }
}

public class GameManager : SingletonBehaviour<GameManager>
{

	#region Resources
	public int Water { get { return items["Water"].amount; } private set { items["Water"].amount = value; } }
	public int Food { get { return items["Food"].amount; } private set { items["Food"].amount = value; } }
	public int Preserved { get { return items["Preserved"].amount; } private set { items["Preserved"].amount = value; } }
	public int Wood { get { return items["Wood"].amount; } private set { items["Wood"].amount = value; } }
	public int Components { get { return items["Components"].amount; } private set { items["Components"].amount = value; } }
	public int Parts { get { return items["Parts"].amount; } private set { items["Parts"].amount = value; } }
	#endregion

	#region Items
	public Dictionary<string, Item> items = new Dictionary<string, Item>();
	#endregion

    #region Furnitures
    //public infoFurniture[] furnitures = new infoFurniture[10];
	public int Table { get; private set; }
	public int Craft { get; private set; }
	public int Bed { get; private set; }
	public int Kitchen { get; private set; }
	public int Bucket { get; private set; }
	public int Door { get; private set; }
	public int Bag { get; private set; }
    public int Refrigerator { get; private set; }
	public Dictionary<string, Furniture> furnitures = new Dictionary<string, Furniture>();
	#endregion

	#region Player Status
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }
    public int Sanity { get; private set; }
    public int MaxSanity { get; private set; }
    public int Energy { get; private set; }
    public int MaxEnergy { get; private set; }
    public int Hunger { get; private set; }
    public int MaxHunger { get; private set; }
    public int Thirst { get; private set; }
    public int MaxThirst { get; private set; }
    private int _experiencePoint = 0;
    public int ExperiencePoint
    {
        get { return _experiencePoint; }
        private set
        {
            _experiencePoint = value;
            if (PlayerLevel == 50)
            {
                _experiencePoint = 0;
            }
            while (_experiencePoint >= maxExperiencePoint[PlayerLevel])
            {
                _experiencePoint -= maxExperiencePoint[PlayerLevel];
                if (PlayerLevel == 49)
                {
                    _experiencePoint = 0;
                }
                ++PlayerLevel;
                ++StatusPoint;
                OnPlayerStatusUpdated();
            }
        }
    }
    public int PlayerLevel { get; private set; }
    private readonly int[] maxExperiencePoint = new int [] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50,999};
    public int StatusPoint { get; private set; }

	private Dictionary<string, Buff> buffs = new Dictionary<string, Buff>();
	public int healthChangePerTurn = 0;
	public int sanityChangePerTurn = 0;
	public int energyChangePerTurn = 0;
	public int hungerChangePerTurn = 0;
	public int thirstChangePerTurn = 0;
	public float statusRecoverConst = 1;
	public float turnPenaltyConst = 1;

	public bool IsGameOver { get { return Health <= 0 || Sanity <= 0; } }
	#endregion

	public bool IsOutside
	{
		get
		{
			return SceneManager.GetActiveScene().name == "Outdoor"
#if UNITY_EDITOR
				|| SceneManager.GetActiveScene().name == "OutdoorBS"
#endif
				;
		}
	}

	public event Action OnResourceUpdated;
	public event Action OnPlayerStatusUpdated;

	public event Action ReservedTask;

	#region Option Variables
	public float turnPassTime = 0.25f;
	#endregion

	[SerializeField]
	private AudioClip clockSFX;

	protected override void Awake()
	{
		if (inst != this)
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
		InitGame();
		TurnManager.inst.OnTurnPassed += StatusUpdateByTurn;
		TurnManager.inst.OnTurnPassed += ApplyBuffs;
		SceneManager.sceneLoaded += OnSceneLoaded;
		PlayerState.Transition(PlayerState.idle);
	}

	private void Start()
	{
		//SaveGameData();
		//LoadGameData();

		OnPlayerStatusUpdated();
		OnResourceUpdated();

		ApplyBuffs(0);
		UIManager.inst.UpdateTimerUI(0);
	}

	private void Update()
	{
		PlayerState.curState.StateUpdate();

		if (Input.GetKeyDown(KeyCode.M))
		{
			ExperiencePoint += 100;
		}
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (!IsOutside)
		{
			foreach (var furniture in furnitures.Values)
			{
				furniture.Level = furniture.Level;
			}
		}
	}

	/// <summary>
	/// Called when 'Start Game' button is clicked
	/// </summary>
	/// <param name="path">Path of saved data. 'null' if new game</param>
	public void Init(string path)
	{
		//TODO : Load data and update gamemanager if path is not null
	}

	#region Resource Functions

	public bool CheckResource(int water = 0, int food = 0, int preserved = 0, int wood = 0, int components = 0, int parts = 0)
	{
		return Water >= water && Food >= food && Preserved >= preserved && Wood >= wood && Components >= components && Parts >= parts;
	}

	public void UseResource(int water = 0, int food = 0, int preserved = 0, int wood = 0, int components = 0, int parts = 0)
	{
		Water = Mathf.Max(Water - water, 0);
		Food = Mathf.Max(Food - food, 0);
		Preserved = Mathf.Max(Preserved - preserved, 0);
		Wood = Mathf.Max(Wood - wood, 0);
		Components = Mathf.Max(Components - components, 0);
		Parts = Mathf.Max(Parts - parts, 0);
		OnResourceUpdated();
	}

	public void GetResource(int water = 0, int food = 0, int preserved = 0, int wood = 0, int components = 0, int parts = 0)
	{
		Water = Mathf.Max(Water + water, 0);
		Food = Mathf.Max(Food + food, 0);
		Preserved = Mathf.Max(Preserved + preserved, 0);
		Wood = Mathf.Max(Wood + wood, 0);
		Components = Mathf.Max(Components + components, 0);
		Parts = Mathf.Max(Parts + parts, 0);
		OnResourceUpdated();
	}

	#endregion

	#region Player Status Functions

	private void StatusUpdateByTurn(int turn)
	{
		ChangePlayerStatus(healthChangePerTurn * turn, sanityChangePerTurn * turn, hungerChangePerTurn * turn, thirstChangePerTurn * turn, energyChangePerTurn * turn);
		OnPlayerStatusUpdated();
		//TODO : Don't use energy while sleeping ;
	}

	private void OutsideStatusUpdateByTurn(int turn)
	{
		DayNight dayNight = TurnManager.inst.DayNight;
	}

    public bool CheckStatus(int health= 0, int mental = 0, int hunger = 0, int thirst = 0, int energy = 0)
    {
        return Health > health && Sanity > mental && Hunger > hunger && Thirst > thirst && Energy > energy;
    }

    public int DisadvantageNeededTurn(float neededTurn)
    {
        if (Health > 50 && Hunger>80)
        {
			return (int)Math.Ceiling(neededTurn);
        }
        else
        {
            return (int)neededTurn;
        }
    }

	public void ChangePlayerStatus(int health = 0, int sanity = 0, int hunger = 0, int thirst = 0, int energy = 0)
	{
		ChangeHealth(health);
		ChangeSanity(sanity);
		ChangeHunger(hunger);
		ChangeThirst(thirst);
		ChangeEnergy(energy);
	}

    public void ChangeHealth(int amount)
    {
        if(amount>0)
        {
            Health = Mathf.Min(MaxHealth, Health + amount);
        }
        else
        {
            Health = Mathf.Max(0, Health + amount);
			if (Health <= 0)
				GameOver();
        }
        OnPlayerStatusUpdated();
    }

    public void IncreaseMaxHealth()
    {
        if(MaxHealth<110&&StatusPoint>0)
        {
            --StatusPoint;
            ++MaxHealth;
            ++Health;
            OnPlayerStatusUpdated();
        }
    }

    public void IncreaseMaxSanity()
    {
        if (MaxSanity < 110 && StatusPoint > 0)
        {
            --StatusPoint;
            ++MaxSanity;
            ++Sanity;
            OnPlayerStatusUpdated();
        }
    }

    public void IncreaseMaxHunger()
    {
        if (MaxHunger < 110 && StatusPoint > 0)
        {
            --StatusPoint;
            ++MaxHunger;
            ++Hunger;
            OnPlayerStatusUpdated();
        }
    }

    public void IncreaseMaxThirst()
    {
        if (MaxThirst < 110 && StatusPoint > 0)
        {
            --StatusPoint;
            ++MaxThirst;
            ++Thirst;
            OnPlayerStatusUpdated();
        }
    }

    public void IncreaseMaxEnergy()
    {
        if (MaxEnergy < 110 && StatusPoint > 0)
        {
            --StatusPoint;
            ++MaxEnergy;
            ++Energy;
            OnPlayerStatusUpdated();
        }
    }

	public void ChangeSanity(int amount)
	{
        if (amount > 0)
        {
            Sanity = Mathf.Min(MaxSanity, Sanity + amount);
        }
        else
        {
            Sanity = Mathf.Max(0, Sanity + amount);
			if (Sanity <= 0)
				GameOver();
		}
        OnPlayerStatusUpdated();
    }

    public void ChangeHunger(int amount)
	{
        if (amount > 0)
        {
            Hunger = Mathf.Min(MaxHunger, Hunger + amount);
        }
        else
        {
            Hunger = Mathf.Max(0, Hunger + amount);
        }
        OnPlayerStatusUpdated();
	}

	public void ChangeThirst(int amount)
	{
        if (amount > 0)
        {
            Thirst = Mathf.Min(MaxThirst, Thirst + amount);
        }
        else
        {
            Thirst = Mathf.Max(0, Thirst + amount);
        }
        OnPlayerStatusUpdated();
    }

	public void ChangeEnergy(int amount)
	{
        if (amount > 0)
        {
            Energy = Mathf.Min(MaxEnergy, Energy + amount);
        }
        else
        {
            Energy = Mathf.Max(0, Energy + amount);
        }
        OnPlayerStatusUpdated();
    }


    public void GetExperiencePoint(int amount)
    {
        ExperiencePoint += amount;
        OnPlayerStatusUpdated();
    }

	public Buff GetBuff(string buffName)
	{
		return buffs[buffName];
	}

	private void ApplyBuffs(int turn)
	{
		healthChangePerTurn = 0;
		sanityChangePerTurn = 0;
		hungerChangePerTurn = -1;
		thirstChangePerTurn = -2;
		energyChangePerTurn = -1;

		turnPenaltyConst = 1;
		statusRecoverConst = 1;

		foreach (var buff in buffs.Values.ToList())
		{
			if (buff.IsActivated)
			{
				buff.Apply(turn);
			}
			UIManager.inst.UpdateBuffUI(buff);
		}
		UIManager.inst.UpdateStatusChangePerTurnUI();
	}

	public List<Disease> GetDiseases()
	{
		List<Disease> diseases = new List<Disease>();
		foreach(var buff in buffs.Values)
		{
			if (buff.GetType().IsSubclassOf(typeof(Disease)) && buff.IsActivated)
				diseases.Add((Disease)buff);
		}
		return diseases;
	}
	#endregion

	#region Game System Functions

	private void GameOver()
	{
		Debug.Log("GameOver!");
		//TODO : Handle gameover event
	}

	#endregion

	public void StartTask(Action task, int neededTurn, bool isDefinite = false)
	{
		ReservedTask = task;
		if (!isDefinite)
		{
			if (turnPenaltyConst > 1)
			{
				neededTurn = Mathf.FloorToInt(turnPenaltyConst * neededTurn);
			}
			else
			{
				neededTurn = Mathf.CeilToInt(turnPenaltyConst * neededTurn);
			}
		}
		PlayerStateWork.remainedTurn = neededTurn;
		PlayerState.Transition(PlayerState.work);
		UIManager.inst.OpenTranslucentPanel();
		SoundManager.inst.PlaySFXLoop(Camera.main.gameObject, clockSFX, Mathf.CeilToInt(turnPassTime));
	}

	public void EndTask()
	{
		UIManager.inst.CloseTranslucentPanel();
		ReservedTask?.Invoke();
	}

	#region Game Data Functions
	public void InitGame()
	{
		foreach (var item in Resources.LoadAll<Item>("Items/"))
		{
			items.Add(item.itemIndexName, item);
			item.Init();
		}
		foreach (var buff in Resources.LoadAll<Buff>("Buffs/"))
		{
			buffs.Add(buff.buffIndexName, buff);
			buff.Init();
		}
		foreach (var furniture in Resources.LoadAll<Furniture>("Furnitures/"))
		{
			furnitures.Add(furniture.furnitureName, furniture);
			furniture.Init();
		}

		Health = 100;
		MaxHealth = 100;
		Sanity = 100;
		MaxSanity = 100;
		Energy = 100;
		MaxEnergy = 100;
		Hunger = 100;
		MaxHunger = 100;
		Thirst = 100;
		MaxThirst = 100;
		PlayerLevel = 0;
		ExperiencePoint = 0;
		StatusPoint = 0;

		Food = Water = Wood = Components = 100;
		Preserved = Parts = 50;
	}

	public void SaveGameData()
	{
		foreach (var item in items.Values)
		{
			JsonHelper.SaveJson(JsonUtility.ToJson(item, true), item.itemIndexName +".json");
		}
		foreach (var buff in buffs.Values)
		{
			JsonHelper.SaveJson(JsonUtility.ToJson(buff, true), buff.buffIndexName + ".json");
		}
	}

	public void LoadGameData()
	{
		foreach (var item in items.Values)
		{
			item.LoadData(JsonHelper.LoadSavedJson(item.itemIndexName + ".json"));
		}
		foreach (var buff in buffs.Values)
		{
			buff.LoadData(JsonHelper.LoadSavedJson(buff.buffIndexName + ".json"));
		}
	}
	#endregion
}