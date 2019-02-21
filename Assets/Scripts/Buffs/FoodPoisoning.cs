﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPoisoning : Disease
{
	public override bool IsActivated
	{
		get
		{
			return remainedTurn > 0;
		}
	}

	public override bool IsCureable
	{
		get
		{
			return true;
		}
	}

	public override void Apply(int turn)
	{
		GameManager.inst.hungerChangePerTurn -= 5;
		GameManager.inst.thirstChangePerTurn -= 5;
	}

	public override void Cure()
	{
		remainedTurn = 0;
	}

	public override void Init()
	{
		TurnManager.inst.OnTurnPassed += OnTurnPassed;
	}
}
