using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpComponents : LogEvent
{
	public int minComponents = 30, maxComponents = 40;
	private int components;

	public override void EventStart()
	{
		components = Random.Range(minComponents, maxComponents + 1);
		UIManager.inst.AddResourceResult(components: components);
		UIManager.inst.AddPlayerStatusResult(energy: -20);
		TurnManager.inst.UseTurn(4);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}
	
	public void Confirm()
	{
		GameManager.inst.GetResource(components: components);
		GameManager.inst.ChangeEnergy(-20);
		MapManager.inst.GetCurrentTileInfo().isHarvested = true;
		OutdoorUIManager.inst.UpdateTileInfoPanel();
		EndEvent();
	}
}
