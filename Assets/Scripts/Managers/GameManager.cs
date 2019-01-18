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
	public int Energy { get; private set; }
	public int Hunger { get; private set; }
	public int Thirst { get; private set; }
	#endregion

	public delegate void VoidEvent();
	public event VoidEvent OnResourceUpdated;
	public event VoidEvent OnPlayerStatusUpdated;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		TurnManager.inst.OnTurnPassed += StatusUpdateByTurn;
	}

	private void Start()
	{
		Energy = 100;
		Hunger = 100;
		Thirst = 100;

		OnPlayerStatusUpdated();
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

	public bool UseResource(int water = 0, int food = 0, int preserved = 0, int wood = 0, int components = 0, int parts = 0)
	{
		if (!UseWater(water))
			return false;
		if (!UseFood(food))
			return false;
		if (!UsePreserved(preserved))
			return false;
		OnResourceUpdated?.Invoke();
		return true;
	}

	private bool UseWater(int amount)
	{
		if (Water < amount)
			return false;
		Water -= amount;
		return true;
	}

	private bool UseFood(int amount)
	{
		if (Food < amount)
			return false;
		Food -= amount;
		return true;
	}

	private bool UsePreserved(int amount)
	{
		if (Preserved < amount)
			return false;
		Preserved -= amount;
		return true;
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
		Hunger = Mathf.Max(0, Hunger - turn);
		Thirst = Mathf.Max(0, Thirst - turn * 3);
		Energy = Mathf.Max(0, Energy - turn);
		OnPlayerStatusUpdated();
	}

	public void Eat(int amount)
	{
		//TODO
	}

	public void Drink(int amount)
	{
		//TODO
	}

	public void Rest(int turn)
	{
		//TODO
	}

	#endregion

	#region Game System Functions

	private void GameOver()
	{
		//TODO : Handle gameover event
	}

	#endregion
}
