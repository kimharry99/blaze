using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Furniture
{
	private readonly int[] waterPerTurn = { 0, 2, 3, 5 };
	private readonly int[] maxCapacity = { 0, 30, 60, 120 };

	public int MaxCapacity { get { return maxCapacity[level]; } }
	public int water;

	private bool isUsing = false;

	public override void Init()
	{
		base.Init();
		water = 0;
		TurnManager.inst.OnTurnPassed += OnTurnPassed;
    }

	public override void OnTurnPassed(int turn)
	{
		if (TurnManager.inst.Weather == Weather.Rain && !isUsing)
		{
			water = Mathf.Min(MaxCapacity, water + turn * waterPerTurn[level]);
		}
	}

	public void HarvestWater()
	{
		GameManager.inst.GetResource(water: water);
		water = 0;
		isUsing = true;
		TurnManager.inst.UseTurn(2);
		isUsing = false;
	}
}
