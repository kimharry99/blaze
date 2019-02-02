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
	None = -1,
	Home,
	Building,
	Store,
	Factory,
	Forest
}

[System.Serializable]
public class TileInfo
{
	public Vector3Int position;

	#region Resources
	public int Water { get; private set; }
	public int Food { get; private set; }
	public int Preserved { get; private set; }
	public int Wood { get; private set; }
	public int Components { get; private set; }
	public int Parts { get; private set; }
	#endregion

	#region Flags
	public bool IsVisited { get; private set; }
	#endregion

	public void Init(int water = 0, int food = 0, int preserved = 0, int wood = 0, int components = 0, int parts = 0, bool isVisited = false)
	{
		Water = water;
		Food = food;
		Preserved = preserved;
		Wood = wood;
		Components = components;
		Parts = parts;
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
		Water = water;
		Food = food;
		Preserved = preserved;
		Wood = wood;
		Components = components;
		Parts = parts;
		IsVisited = isVisited;
		this.landType = landType;
		this.structureType = structureType;
	}

	public void TakeResources()
	{
		int water = Random.Range(0, Water + 1);
		int food = Random.Range(0, Food + 1);
		int preserved = Random.Range(0, Preserved + 1);
		int wood = Random.Range(0, Wood + 1);
		int components = Random.Range(0, Components + 1);
		int parts = Random.Range(0, Parts + 1);

		GameManager.inst.GetResource(water: water, food: food, preserved: preserved, wood: wood, components: components, parts: parts);
		Water -= water;
		Food -= food;
		Preserved -= preserved;
		Wood -= wood;
		Components -= components;
		Parts -= parts;
	}
}
