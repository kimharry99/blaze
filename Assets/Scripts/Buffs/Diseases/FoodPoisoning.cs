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

	public override void AddNewDisease()
	{
		remainedTurn += 24;
		UIManager.inst.UpdateBuffUI(this);
	}

	public override void Apply(int turn)
	{
		GameManager.inst.hungerChangePerTurn -= 5;
		GameManager.inst.thirstChangePerTurn -= 5;
	}

	public override void Cure()
	{
		base.Cure();
		remainedTurn = 0;
	}

	public override void Init()
	{
		base.Init();
		TurnManager.inst.OnTurnPassed += OnTurnPassed;
	}
}
