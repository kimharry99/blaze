using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FarmState
{
	Idle,
	Grow,
	Harvest
}

public class Farm : Furniture
{
	public int turnLeft;
	private FarmState state;

	private void Start()
	{
		GameManager.inst.OnTurnPassed += OnTurnPassed;
	}

	private void OnTurnPassed(int turn)
	{
		if (state != FarmState.Grow)
			return;
		turnLeft -= turn;
		if (turnLeft <= 0)
		{
			state = FarmState.Harvest;
		}
	}

	public override void UseFurniture()
	{
		if (state == FarmState.Idle)
		{
			if (!GameManager.inst.UseResource(water: 30))
				return;
			turnLeft = Random.Range(20, 31);
			state = FarmState.Grow;
			GameManager.inst.UseTurn(4);
		}
		if (state == FarmState.Harvest)
		{
			GameManager.inst.GetResource(food:Random.Range(30, 40));
			state = FarmState.Idle;
			GameManager.inst.UseTurn(2);
		}
	}

	private void OnDestroy()
	{
		GameManager.inst.OnTurnPassed -= OnTurnPassed;
	}
}
