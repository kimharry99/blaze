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
	protected override FurnitureType type { get { return FurnitureType.Farm; } }
	public int[] turnLeft = new int[3];
	private FarmState[] state = new FarmState[3];

	private int i = 0;
	private int option = 0;

	private void Start()
	{
		TurnManager.inst.OnTurnPassed += OnTurnPassed;
	}

	private void OnTurnPassed(int turn)
	{
		for (int i = 0; i < Level; ++i)
		{
			if (state[i] != FarmState.Grow)
				return;
			turnLeft[i] -= turn;
			if (turnLeft[i] <= 0)
			{
				state[i] = FarmState.Harvest;
			}
		}
	}

	public void UseFurniture()
	{
		if (state[i] == FarmState.Idle)
		{
			if (!GameManager.inst.UseResource(water: 30))
				return;
			turnLeft[i] = Random.Range(20, 31);
			state[i] = FarmState.Grow;
		}
		if (state[i] == FarmState.Harvest)
		{
			GameManager.inst.GetResource(food:Random.Range(30, 40));
			state[i] = FarmState.Idle;
		}
	}

	public override void OpenFurnitureUI()
	{
		furnitureUI.SetActive(true);
	}

	private void OnDestroy()
	{
		TurnManager.inst.OnTurnPassed -= OnTurnPassed;
	}
}
