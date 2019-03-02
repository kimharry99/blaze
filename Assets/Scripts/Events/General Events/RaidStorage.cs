using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaidStorage : LogEvent
{
	public int minResource = 10, maxResource = 20;
	private int resource;

	public override void EventStart()
	{
		resource = Random.Range(minResource, maxResource + 1);
		UIManager.inst.AddResourceResult(resource, resource, resource, resource, resource, resource);
		UIManager.inst.AddPlayerStatusResult(energy: -20);
		TurnManager.inst.UseTurn(4);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	private void Confirm()
	{
		GameManager.inst.GetResource(resource, resource, resource, resource, resource, resource);
		GameManager.inst.ChangeEnergy(-20);
		MapManager.inst.GetCurrentTileInfo().isHarvested = true;
		OutdoorUIManager.inst.UpdateTileInfoPanel();
		EndEvent();
	}
}
