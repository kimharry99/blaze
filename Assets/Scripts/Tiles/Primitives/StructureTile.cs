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
		UIManager.inst.OpenEventLogPanel(EventManager.inst.GetEvent("Explore"));
	}

	private void Rest()
	{
		TurnManager.inst.UseTurn(4);
		GameManager.inst.Rest(RestAmount);
	}

	public StructureTileInfo GetStructureTileInfo(Vector3Int position)
	{
		return new StructureTileInfo(position, Type);
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
