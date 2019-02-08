using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class XMark_Box : LogEvent
{
	int food, preserved, water;
	public override void EventStart()
	{
		food = Random.Range(10, 16);
		preserved = Random.Range(0, 6);
		water = Random.Range(10, 16);
		UIManager.inst.AddResourceResult(food: food, preserved: preserved, water: water);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	private void Confirm()
	{
		GameManager.inst.GetResource(water: water, food: food, preserved: preserved);
		EndEvent();
	}

}
