using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HarvestFood : LogEvent
{
	public int minFood = 30, maxFood = 40;
	private int food;

	public override void EventStart()
	{
		food = Random.Range(minFood, maxFood + 1);
		UIManager.inst.AddResourceResult(food: food);
		UIManager.inst.AddPlayerStatusResult(energy: -20);
		TurnManager.inst.UseTurn(4);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	public void Confirm()
	{
		GameManager.inst.GetResource(food: food);
		GameManager.inst.ChangeEnergy(-20);
		MapManager.inst.GetCurrentTileInfo().isHarvested = true;
		OutdoorUIManager.inst.UpdateTileInfoPanel();
		EndEvent();
	}
}
