using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fire_Success : LogEvent
{
	public int turn = 4;
	public int minFood = 5, maxFood = 10;
	public int minWood = 10, maxWood = 20;
	private int food, wood;
	public override void EventStart()
	{
		TurnManager.inst.UseTurn(turn);
		food = Random.Range(minFood, maxFood + 1);
		wood = Random.Range(minWood, maxWood + 1);
		UIManager.inst.AddResourceResult(food: food, wood: wood);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	private void Confirm()
	{
		GameManager.inst.GetResource(food: food, wood: wood);
		EndEvent();
	}
}
