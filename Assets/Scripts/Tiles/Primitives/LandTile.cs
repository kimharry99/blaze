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
	#region Resources
	public int Water { get; private set; }
	public int Food { get; private set; }
	public int Preserved { get; private set; }
	public int Wood { get; private set; }
	public int Components { get; private set; }
	public int Parts { get; private set; }
	#endregion
	public abstract int MoveCost { get; }
	public bool IsVisited { get; private set; }

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

	/// <summary>
	/// Take Resources from tile. 
	/// </summary>
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

	public LandTileInfo GetLandTileInfo(Vector3Int position)
	{
		return new LandTileInfo(position, Water, Food, Preserved, Wood, Components, Parts, GetType().ToString());
	}

	public override void OnVisited()
	{
		IsVisited = true;
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
	public string type;
	public bool isVisited;

	public LandTileInfo(Vector3Int position, int water, int food, int preserved, int wood, int components, int parts, string type, bool isVisited = false)
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