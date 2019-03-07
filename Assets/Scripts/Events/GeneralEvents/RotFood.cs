using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RotFood : LogEvent
{
	private int food;
	public override void EventStart()
	{
		food = -Mathf.CeilToInt(GameManager.inst.Food * ((Refrigerator)GameManager.inst.furnitures["Refrigerator"]).LostFoodRate / 100);
		UIManager.inst.AddResourceResult(food: food);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	private void Confirm()
	{
		GameManager.inst.GetResource(food: food);
		EndEvent();
	}
}
