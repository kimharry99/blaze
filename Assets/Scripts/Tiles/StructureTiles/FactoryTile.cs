﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FactoryTile : StructureTile
{
	public override int RestAmount { get { return 20; } }

	public override void OnVisited(Vector3Int pos)
	{
		base.OnVisited(pos);

	}

	public override List<UnityAction> GetTileActions()
	{
		List<UnityAction> actions = base.GetTileActions();
		if (!MapManager.inst.GetCurrentTileInfo().isHarvested)
			actions.Add(DisassembleMachine);
		return actions;
	}

	public void DisassembleMachine()
	{
		EventManager.inst.StartEvent("DisassembleMachine");
	}
}
