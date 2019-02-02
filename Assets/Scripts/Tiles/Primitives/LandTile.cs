using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Primitive class for all land type tiles in this game
/// </summary>
[System.Serializable]
public abstract class LandTile : DefaultTile
{
	public abstract int MoveCost { get; }
	public abstract LandType type { get; }

	public override void OnVisited()
	{
		
	}
}

[System.Serializable]
public class LandTileInfo
{
	public Vector3Int position;
	public int water;
	public int food;
	public int preserved;
	public int wood;
	public int components;
	public int parts;
	public LandType type;
	public bool isVisited;

	public LandTileInfo(Vector3Int position, int water, int food, int preserved, int wood, int components, int parts, LandType type, bool isVisited = false)
	{
		this.position = position;
		this.water = water;
		this.food = food;
		this.preserved = preserved;
		this.wood = wood;
		this.components = components;
		this.parts = parts;
		this.type = type;
		this.isVisited = false;
	}
}