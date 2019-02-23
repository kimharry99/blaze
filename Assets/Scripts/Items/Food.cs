using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Food : Item, IConsumableItem
{
	public List<int> remainedTurns = new List<int>();

	public int health;
	public int sanity;
	public int hunger;
	public int thirst;
	public int energy;

	public bool IsUsable
	{
		get
		{
			//TODO : Cannot eat when hunger is over 80% of maxhunger
			return true;
		}
	}

	public override void Init()
	{
		TurnManager.inst.OnTurnPassed += OnTurnPassed;
	}

	public void Use()
	{
		GameManager.inst.ChangePlayerStatus(health, sanity, hunger, thirst, energy);
	}

	public void Use(int option)
	{
		Use();
		remainedTurns.Remove(option);
		amount = remainedTurns.Count;
	}

	public void AddNewFood()
	{
		remainedTurns.Add(96 * 3);
		amount = remainedTurns.Count;
	}

	public void AddNewFood(int remainedTurn)
	{
		remainedTurns.Add(remainedTurn);
		amount = remainedTurns.Count;
	}

	private void OnTurnPassed(int turn)
	{
		for (int i = 0; i < amount; ++i)
		{
			remainedTurns[i] -= turn;
		}
		remainedTurns.RemoveAll(i => i <= 0);
		amount = remainedTurns.Count;
	}
}
