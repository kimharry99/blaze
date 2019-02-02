using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Primitive class for all structure type tiles in this game
/// </summary>
public abstract class StructureTile : DefaultTile
{
	protected abstract void OpenOptions();
	public abstract StructureType Type { get; }

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
