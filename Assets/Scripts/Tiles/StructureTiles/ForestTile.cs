﻿using System.Collections;
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
	}

	public override List<UnityAction> GetTileActions()
	{
		List<UnityAction> actions = base.GetTileActions();
		return actions;
	}
}
