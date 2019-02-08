using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ForestTile : StructureTile
{
	public override StructureType Type { get { return StructureType.Forest; } }

	public override int RestAmount { get { return 15; } }

	public override void OnVisited(Vector3Int pos)
	{
		base.OnVisited(pos);
		if (!MapManager.inst.tileInfos[pos].isVisited)
		{
			float rand = Random.Range(0, 100);
			if (rand < 10)
				UIManager.inst.OpenEventLogPanel(EventManager.inst.GetEvent("XMark"));
		}
		
	}

	public override void OnExplored()
	{
		base.OnExplored();
		float rand = Random.Range(0, 100);
		if (rand < 5)
			UIManager.inst.OpenEventLogPanel(EventManager.inst.GetEvent("XMark"));
	}

	public override List<UnityAction> GetTileActions()
	{
		List<UnityAction> actions = base.GetTileActions();
		return actions;
	}
}
