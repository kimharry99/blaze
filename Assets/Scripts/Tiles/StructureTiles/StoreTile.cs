using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoreTile : StructureTile
{
	public override int RestAmount { get { return 30; } }

	public override void OnVisited(Vector3Int pos)
	{
		base.OnVisited(pos);
	}

	public override List<UnityAction> GetTileActions()
	{
		List<UnityAction> actions = base.GetTileActions();
		if (!MapManager.inst.GetCurrentTileInfo().isHarvested)
			actions.Add(RaidStorage);
		return actions;
	}

	private void RaidStorage()
	{
		EventManager.inst.StartEvent("RaidStorage");
	}
}
