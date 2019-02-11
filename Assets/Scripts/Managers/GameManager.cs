using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager>
{

	#region Resources
	public int Water { get; private set; }
	public int Food { get; private set; }
	public int Preserved { get; private set; }
	public int Wood { get; private set; }
	public int Components { get; private set; }
	public int Parts { get; private set; }
	#endregion

	#region Items
	public Dictionary<int, Item> items = new Dictionary<int, Item>();
	public Dictionary<int, Item> bag = new Dictionary<int, Item>();
	public float BagWeight
	{
		get
		{
			//TODO : Calculate weight of all items in the bag and return it
			return 0;
		}
	}
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
	#endregion

	#region Player Status
    public int Health { get; private set; }
    public int Mental { get; private set; }
    public int Energy { get; private set; }
	public int Hunger { get; private set; }
	public int Thirst { get; private set; }
	#endregion

	public event Action OnResourceUpdated;
	public event Action OnPlayerStatusUpdated;

	public event Action ReservedTask;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		TurnManager.inst.OnTurnPassed += StatusUpdateByTurn;
		PlayerState.Transition(PlayerState.idle);
	}

	private void Start()
	{
        Health = 100;
        Mental = 100;
		Energy = 100;
		Hunger = 100;
		Thirst = 100;

		OnPlayerStatusUpdated();
	}

	private void Update()
	{
		PlayerState.curState.StateUpdate();
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
        if(Hunger >80)
        {
            Mental = Mathf.Min(100, Mental + 2 * turn);
        }
        else if (Hunger == 0)
        {
            Health = Mathf.Max(0, Health - 4 * turn);
            Mental = Mathf.Max(0, Mental - 2 * turn);
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
            Mental = Mathf.Max(0, Mental - 2 * turn);
        }
		Energy = Mathf.Max(0, Energy - turn);
        OnPlayerStatusUpdated();

		//TODO : Don't use energy while sleeping ;
	}

    public bool CheckStatus(int health= 0, int mental = 0, int hunger = 0, int thirst = 0, int energy = 0)
    {
        return Health > health && Mental > mental && Hunger > hunger && Thirst > thirst && Energy > energy;
    }

    public float CalculateTurnPenalty()
    {
        if (Health>50)
        {
            if (Hunger>80)
            {
                return 0.5f;
            }
            else
            {
                return 1;
            }
        }
        else if (Health>25)
        {
            if (Hunger>80)
            {
                return 1;
            }
            else
            {
                return 1.5f;
            }
        }
        else
        {
            if (Hunger>80)
            {
                return 1.5f;
            }
            else
            {
                return 2;
            }
        }
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

    public void Cure(int amount)
    {
        if(amount>0)
        {
            Health = Mathf.Min(100, Health + amount);
        }
        else
        {
            Health = Mathf.Max(0, Health + amount);
        }
        OnPlayerStatusUpdated();
    }

    public void Heal (int amount)
    {
        if (amount > 0)
        {
            Mental = Mathf.Min(100, Mental + amount);
        }
        else
        {
            Mental = Mathf.Max(0, Mental + amount);
        }
        OnPlayerStatusUpdated();
    }

    public void Eat(int amount)
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

	public void Drink(int amount)
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

	public void Rest(int amount)
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

	#endregion

	#region Game System Functions

	private void GameOver()
	{
		//TODO : Handle gameover event
	}

	#endregion

	public void StartTask(Action task, int neededTurn, bool isDefinite = false)
	{
		ReservedTask = task;
		if (!isDefinite)
			neededTurn = DisadvantageNeededTurn(CalculateTurnPenalty() * neededTurn);
		PlayerStateWork.remainedTurn = neededTurn;
		PlayerState.Transition(PlayerState.work);
	}

	public void EndTask()
	{
		ReservedTask();
	}
}
