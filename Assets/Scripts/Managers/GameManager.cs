using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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
	public List<Food> foods = new List<Food>();
	#endregion

	#region Furnitures
	public int Table { get; private set; }
	public int Craft { get; private set; }
	public int Bed { get; private set; }
	public int Garden { get; private set; }
	public int Kitchen { get; private set; }
	public int Bucket { get; private set; }
	public int Door { get; private set; }
	public int Bag { get; private set; }
	public int WareHouse { get; private set; }
    public int Refrigerator { get; private set; }
	public Dictionary<string, Furniture> furnitures = new Dictionary<string, Furniture>();
	#endregion

	#region Player Status
    public int Health { get; private set; }
    public int Sanity { get; private set; }
    public int Energy { get; private set; }
	public int Hunger { get; private set; }
	public int Thirst { get; private set; }

	[SerializeField]
	//private List<Buff> buffs = new List<Buff>();
	private Dictionary<string, Buff> buffs = new Dictionary<string, Buff>();
	public int healthChangePerTurn = 0;
	public int sanityChangePerTurn = 0;
	public int energyChangePerTurn = 0;
	public int hungerChangePerTurn = 0;
	public int thirstChangePerTurn = 0;
	public float statusRecoverConst = 1;
	public float turnPenaltyConst = 1;
	#endregion

	public bool IsOutside
	{
		get
		{
			return SceneManager.GetActiveScene().name == "OutdoorBS";
		}
	}

	public event Action OnResourceUpdated;
	public event Action OnPlayerStatusUpdated;

	public event Action ReservedTask;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		TurnManager.inst.OnTurnPassed += StatusUpdateByTurn;
		TurnManager.inst.OnTurnPassed += ApplyBuffs;
		PlayerState.Transition(PlayerState.idle);
	}

	private void Start()
	{
        Health = 100;
        Sanity = 100;
		Energy = 100;
		Hunger = 100;
		Thirst = 100;

		InitGame();
		SaveGameData();
		//LoadGameData();

		ApplyBuffs(1);

		OnPlayerStatusUpdated();
		OnResourceUpdated();
	}

	private void Update()
	{
		PlayerState.curState.StateUpdate();

		if (Input.GetKeyDown(KeyCode.M))
		{
			Hunger = 0;
			Thirst = 0;
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
		Water -= water;
		Food -= food;
		Preserved -= preserved;
		Wood -= wood;
		Components -= components;
		Parts -= parts;
		OnResourceUpdated();
	}

	public void GetResource(int water = 0, int food = 0, int preserved = 0, int wood = 0, int components = 0, int parts = 0)
	{
		Water += water;
		Food += food;
		Preserved += preserved;
		Wood += wood;
		Components += components;
		Parts += parts;
		OnResourceUpdated();
	}

	#endregion

	#region Player Status Functions

	private void StatusUpdateByTurn(int turn)
	{
		/*
        if(Hunger >80)
        {
            Sanity = Mathf.Min(100, Sanity + 2 * turn);
        }
        else if (Hunger == 0)
        {
            Health = Mathf.Max(0, Health - 4 * turn);
            Sanity = Mathf.Max(0, Sanity - 2 * turn);
        }
		Hunger = Mathf.Max(0, Hunger - turn);
        if (Thirst > 80)
        {
            Thirst = Mathf.Max(0, Thirst - turn);
            Health = Mathf.Min(100, Health + turn);
        }
        else if (0<Thirst && Thirst <81)
        {
            Thirst = Mathf.Max(0, Thirst - 2 * turn);
        }
        else
        {
            Health = Mathf.Max(0, Health - 4 * turn);
            Sanity = Mathf.Max(0, Sanity - 2 * turn);
        }
		Energy = Mathf.Max(0, Energy - turn);
        OnPlayerStatusUpdated();
		*/
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
        if (Health > 50&&Hunger>80)
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
            Health = Mathf.Min(100, Health + amount);
        }
        else
        {
            Health = Mathf.Max(0, Health + amount);
			if (Health <= 0)
				GameOver();
        }
        OnPlayerStatusUpdated();
    }

    public void ChangeSanity (int amount)
    {
        if (amount > 0)
        {
            Sanity = Mathf.Min(100, Sanity + amount);
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
            Hunger = Mathf.Min(100, Hunger + amount);
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
            Thirst = Mathf.Min(100, Thirst + amount);
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
            Energy = Mathf.Min(100, Energy + amount);
        }
        else
        {
            Energy = Mathf.Max(0, Energy + amount);
        }
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
	}

	public void EndTask()
	{
		ReservedTask();
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
	}

	public void SaveGameData()
	{
		foreach (var item in items.Values)
		{
			JsonHelper.JsonToFile(JsonUtility.ToJson(item, true), "Save/Items/" + item.itemIndexName +".json");
		}
		foreach (var buff in buffs.Values)
		{
			JsonHelper.JsonToFile(JsonUtility.ToJson(buff, true), "Save/Buffs/" + buff.buffIndexName + ".json");
		}
	}

	public void LoadGameData()
	{
		foreach (var item in items.Values)
		{
			item.LoadData(JsonHelper.LoadJson("Save/Items/" + item.itemIndexName));
		}
		foreach (var buff in buffs.Values)
		{
			buff.LoadData(JsonHelper.LoadJson("Save/Buffs/" + buff.buffIndexName));
		}
	}
	#endregion
}