using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

/// <summary>
/// Primitive class for all structure type tiles in this game
/// </summary>
public abstract class StructureTile : DefaultTile
{
	public abstract StructureType Type { get; }
	public abstract int RestAmount { get; }

	public virtual List<UnityAction> GetTileActions()
	{
		List<UnityAction> actions = new List<UnityAction>
		{
			Explore,
			Rest
		};
		return actions;
	}

	private void Explore()
	{
		TurnManager.inst.UseTurn(4);
		int water, food, preserved, wood, components, parts;
		water = food = preserved = wood = components = parts = 0;
		MapManager.inst.tileInfos[MapManager.inst.curPosition].TakeResources(ref food, ref preserved, ref water, ref wood, ref components, ref parts);
		UIManager.inst.AddResourceResult(food, preserved, water, wood, components, parts);

		List<string> actionDescriptions = new List<string> { "확인" };
		UnityAction action = UIManager.inst.CloseEventLogPanel;

		UIManager.inst.OpenEventLogPanel(EventManager.inst.GetEventInfo("Explore"), new List<UnityAction> { UIManager.inst.CloseEventLogPanel });
		/*
		UIManager.inst.OpenEventLogPanel("탐색 결과", "탐색을 완료했습니다.\n다음과 같은 보상을 획득하였습니다!",
		new List<UnityAction>{ UIManager.inst.CloseEventLogPanel },
		new List<string> { "확인" });
		*/
		OutdoorUIManager.inst.UpdateTileInfoPanel();
	}

	private void Rest()
	{
		TurnManager.inst.UseTurn(4);
		GameManager.inst.Rest(RestAmount);
	}

	private void A(int a)
	{
		
	}
	public StructureTileInfo GetStructureTileInfo(Vector3Int position)
	{
		return new StructureTileInfo(position, Type);
	}
}

[System.Serializable]
public class StructureTileInfo
{
	public Vector3Int position;
	public StructureType type;

	public StructureTileInfo(Vector3Int position, StructureType type)
	{
		this.position = position;
		this.type = type;
	}
}
