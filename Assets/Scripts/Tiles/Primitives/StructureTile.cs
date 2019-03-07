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
	public StructureType type;
	public abstract int RestAmount { get; }
	public List<string> actionNames = new List<string> { "1.탐험하기", "2.쉬어가기" };
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
		EventManager.inst.StartEvent("Explore");
	}

	private void Rest()
	{
		TurnManager.inst.UseTurn(4);
		EventManager.inst.StartEvent("Rest");
	}

	public StructureTileInfo GetStructureTileInfo(Vector3Int position)
	{
		return new StructureTileInfo(position, type);
	}

	public override void OnVisited(Vector3Int pos)
	{
		
	}

	public virtual void OnExplored()
	{

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
