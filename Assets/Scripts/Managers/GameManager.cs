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
	#endregion

	#region Player Status
    public int Health { get; private set; }
    public int Mental { get; private set; }
    public int Energy { get; private set; }
	public int Hunger { get; private set; }
	public int Thirst { get; private set; }
	#endregion

	public delegate void VoidEvent();
	public event VoidEvent OnResourceUpdated;
	public event VoidEvent OnPlayerStatusUpdated;

	public event VoidEvent ReservedTask;

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
        if (Thirst > 60)
        {
            Thirst = Mathf.Max(0, Thirst - turn);
            Health = Mathf.Min(100, Health + turn);
        }
        else if (0<Thirst && Thirst <61)
        {
            Thirst = Mathf.Max(0, Thirst - 2 * turn);
        }
        else if(Thirst == 0)
        {
            Health = Mathf.Max(0, Health - 4 * turn);
            Mental = Mathf.Max(0, Mental - 2 * turn);
        }
		Energy = Mathf.Max(0, Energy - turn);
		OnPlayerStatusUpdated();


        print(Health+" "+Mental+" "+Hunger + " "+Thirst + " "+Energy);
		//TODO : Hunger < 0 Thirst < 0 Down Health, Mental
	}

	public void Eat(int amount)
	{
        //TODO
        Hunger = Mathf.Min(100, Hunger + amount);
	}

	public void Drink(int amount)
	{
        //TODO
        Thirst = Mathf.Min(100, Thirst + amount);
	}

	public void Rest(int amount)
	{
        //TODO
        Energy = Mathf.Min(100, Energy + amount);
	}

	#endregion

	#region Game System Functions

	private void GameOver()
	{
		//TODO : Handle gameover event
	}

	#endregion

	public void StartTask(VoidEvent task, int neededTurn)
	{
		ReservedTask = task;
		PlayerStateWork.remainedTurn = neededTurn;
		PlayerState.Transition(PlayerState.work);
	}

	public void EndTask()
	{
		ReservedTask();
	}
}
