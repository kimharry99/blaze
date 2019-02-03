using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Primitive class for all tiles in this game
/// </summary>
public abstract class DefaultTile : Tile
{
	public abstract void OnVisited();
}

public enum LandType
{
	Road,
	Field,
	Hill,
	Mountain
}

public enum StructureType
{
	Home,
	Building,
	Store,
	Factory,
	Forest,
	None
}

[System.Serializable]
public class TileInfo
{
	public Vector3Int position;

	#region Resources
	public int water;
	public int food;
	public int preserved;
	public int wood;
	public int components;
	public int parts;
	#endregion

	#region Flags
	public bool IsVisited { get; private set; }
	#endregion

	public void Init(int water = 0, int food = 0, int preserved = 0, int wood = 0, int components = 0, int parts = 0, bool isVisited = false)
	{
		this.water = water;
		this.food = food;
		this.preserved = preserved;
		this.wood = wood;
		this.components = components;
		this.parts = parts;
		IsVisited = isVisited;
	}

	public LandType landType;
	public StructureType structureType;

	public TileInfo(Vector3Int position, LandType landType, StructureType structureType)
	{
		this.position = position;
		this.landType = landType;
		this.structureType = structureType;
	}

	public TileInfo(Vector3Int position, int water, int food, int preserved, int wood, int components, int parts, string type, LandType landType, StructureType structureType, bool isVisited = false)
	{
		this.position = position;
		this.water = water;
		this.food = food;
		this.preserved = preserved;
		this.wood = wood;
		this.components = components;
		this.parts = parts;
		IsVisited = isVisited;
		this.landType = landType;
		this.structureType = structureType;
	}

	public void TakeResources()
	{
		int water = Random.Range(0, this.water + 1);
		int food = Random.Range(0, this.food + 1);
		int preserved = Random.Range(0, this.preserved + 1);
		int wood = Random.Range(0, this.wood + 1);
		int components = Random.Range(0, this.components + 1);
		int parts = Random.Range(0, this.parts + 1);

		GameManager.inst.GetResource(water: water, food: food, preserved: preserved, wood: wood, components: components, parts: parts);
		this.water -= water;
		this.food -= food;
		this.preserved -= preserved;
		this.wood -= wood;
		this.components -= components;
		this.parts -= parts;
	}
}
