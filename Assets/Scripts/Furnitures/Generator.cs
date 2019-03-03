using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Furniture
{
	public int[] woodNeeded;
	public int[] partsNeeded;
	public int[] neededTurn;
	public int option = 0;
	public int remainedTurn = 0;
	public bool isFinished = false;

	public override void OnTurnPassed(int turn)
	{
		remainedTurn -= turn;
		if (remainedTurn <= 0)
		{
			remainedTurn = 0;
			TurnManager.inst.OnTurnPassed -= OnTurnPassed;
			isFinished = true;
		}
	}

	public void Use()
	{
		GameManager.inst.UseResource(wood: woodNeeded[option], parts: partsNeeded[option]);
		remainedTurn = neededTurn[option];
		TurnManager.inst.OnTurnPassed += OnTurnPassed;
	}

	public void Harvest()
	{
		GameManager.inst.items["Battery"].amount += option + 1;
		isFinished = false;
	}
}
