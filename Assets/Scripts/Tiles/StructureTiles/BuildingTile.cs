using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class BuildingTile : StructureTile
{
	public override StructureType Type { get { return StructureType.Building; } }

	public override int RestAmount { get { return 30; } }

	public override void OnVisited(Vector3Int pos)
	{
		base.OnVisited(pos);
		UIManager.inst.OpenEventLogPanel(EventManager.inst.GetEvent("WeirdSound"));
	}

	public override List<UnityAction> GetTileActions()
	{
		List<UnityAction> actions = base.GetTileActions();
		return actions;
	}
}
