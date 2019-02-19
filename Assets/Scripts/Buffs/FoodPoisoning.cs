using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPoisoning : Buff
{
	public override bool IsActivated
	{
		get
		{
			return RemainedTurn > 0;
		}
	}

	public override void Apply(int turn)
	{
		GameManager.inst.hungerChangePerTurn -= 5;
		GameManager.inst.thirstChangePerTurn -= 5;
	}

	public override void Init()
	{
		TurnManager.inst.OnTurnPassed += OnTurnPassed;
	}
}
