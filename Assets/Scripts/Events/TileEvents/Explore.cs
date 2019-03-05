using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Explore : LogEvent
{
	private int water, food, preserved, wood, components, parts;

	public override void EventStart()
	{
		TurnManager.inst.UseTurn(4);

		TileInfo info = MapManager.inst.tileInfos[MapManager.inst.curPosition];
		water = Random.Range(0, info.water + 1);
		food = Random.Range(0, info.food + 1);
		preserved = Random.Range(0, info.preserved + 1);
		wood = Random.Range(0, info.wood + 1);
		components = Random.Range(0, info.components + 1);
		parts = Random.Range(0, info.parts + 1);

		UIManager.inst.AddResourceResult(food, preserved, water, wood, components, parts);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	private void Confirm()
	{
		MapManager.inst.tileInfos[MapManager.inst.curPosition].TakeResources(food, preserved, water, wood, components, parts);
		UIManager.inst.CloseEventLogPanel();
		OutdoorUIManager.inst.UpdateTileInfoPanel();
		MapManager.inst.structureTilemap.GetTile<StructureTile>(MapManager.inst.curPosition).OnExplored();
	}
}
