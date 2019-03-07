using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rest : LogEvent
{
	private int restAmount;
	public override void EventStart()
	{
		restAmount = MapManager.inst.structureTilemap.GetTile<StructureTile>(MapManager.inst.curPosition).RestAmount;
		UIManager.inst.AddPlayerStatusResult(energy: restAmount);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	private void Confirm()
	{
		GameManager.inst.ChangeEnergy(restAmount);
		EndEvent();
	}
}
