using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FarmTile : StructureTile
{
	public override int RestAmount { get { return 15; } }

	public override void OnVisited(Vector3Int pos)
	{
		base.OnVisited(pos);
	}

	public override List<UnityAction> GetTileActions()
	{
		List<UnityAction> actions = base.GetTileActions();
		if (!MapManager.inst.GetCurrentTileInfo().isHarvested)
			actions.Add(HarvestFood);
		return actions;
	}

	public void HarvestFood()
	{
		EventManager.inst.StartEvent("HarvestFood");
	}
}
