using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Furniture
{
	public override FurnitureType type { get { return FurnitureType.Bucket; } }

	private readonly int[] waterPerTurn = { 2, 3, 5 };
	private readonly int[] maxCapacity = { 30, 60, 120 };

	public int MaxCapacity { get { return maxCapacity[Level]; } }
	public int Water { get; private set; }

	private void Start()
	{
		TurnManager.inst.OnTurnPassed += OnTurnPassed;
        GameManager.inst.furnitures[(int)type] = new infoFurniture(Level, false);
    }

	public override void OnUseButtonClicked()
	{
		if (Water > 0)
			GameManager.inst.StartTask(UseFurniture, Mathf.CeilToInt(Water / 30f));
	}

	public void UseFurniture()
	{
		GameManager.inst.GetResource(water: Water);
		Water = 0;
	}

	public void OnTurnPassed(int turn)
	{
		if (TurnManager.inst.Weather == Weather.Rain)
		{
			Water = Mathf.Min(MaxCapacity, Water + turn * waterPerTurn[Level]);
		}
	}
}
