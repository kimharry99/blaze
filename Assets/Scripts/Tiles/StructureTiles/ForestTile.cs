﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ForestTile : StructureTile
{
	public override int RestAmount { get { return 15; } }

	public override void OnVisited(Vector3Int pos)
	{
		base.OnVisited(pos);
		if (!MapManager.inst.tileInfos[pos].isVisited)
		{
			float rand = Random.Range(0, 100);
			if (rand < 10)
			{
				UIManager.inst.OpenEventLogPanel(EventManager.inst.GetEvent("XMark"));
				return;
			}
			rand = Random.Range(0, 100);
			if (rand < 2)
			{
				UIManager.inst.OpenEventLogPanel(EventManager.inst.GetEvent("TrappedDeer"));
				return;
			}
			rand = Random.Range(0, 100);
			if (rand < 2)
			{
				UIManager.inst.OpenEventLogPanel(EventManager.inst.GetEvent("Fire"));
				return;
			}
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
		if (!MapManager.inst.GetCurrentTileInfo().isHarvested)
			actions.Add(WoodCutting);
		return actions;
	}

	private void WoodCutting()
	{
		EventManager.inst.StartEvent("WoodCutting");
	}
}
