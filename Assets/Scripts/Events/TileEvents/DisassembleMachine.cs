using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DisassembleMachine : LogEvent
{
	public int minParts= 20, maxParts = 30;
	private int parts;

	public override void EventStart()
	{
		parts = Random.Range(minParts, maxParts + 1);
		UIManager.inst.AddResourceResult(parts: parts);
		UIManager.inst.AddPlayerStatusResult(energy: -20);
		TurnManager.inst.UseTurn(4);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	public void Confirm()
	{
		GameManager.inst.GetResource(parts: parts);
		GameManager.inst.ChangeEnergy(-20);
		MapManager.inst.GetCurrentTileInfo().isHarvested = true;
		OutdoorUIManager.inst.UpdateTileInfoPanel();
		EndEvent();
	}
}
