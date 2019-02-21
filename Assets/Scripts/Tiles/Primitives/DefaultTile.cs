using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Primitive class for all tiles in this game
/// </summary>
public abstract class DefaultTile : Tile
{
	public string tileName;
	public abstract void OnVisited(Vector3Int pos);
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
	public bool isVisited;
	#endregion

	public Color TileColor {
		get
		{
			Color color;
			Color timeColor = Color.white;
			switch (TurnManager.inst.DayNight)
			{
				case DayNight.Day:
					timeColor = Color.white;
					break;
				case DayNight.Sunset:
					timeColor = new Color(1, 0.5f, 0);
					break;
				case DayNight.Night:
					timeColor = Color.black;
					break;
			}

			if (MapManager.inst.IsNearByTile(position, MapManager.inst.SightDistance))
			{
				color = (Color.white + timeColor) / 2;
			}
			else
			{
				if (isVisited)
				{
					color = (Color.gray + timeColor) / 2;
				}
				else
				{
					color = Color.black;
				}
			}
			return color;
		}
	}

	public void Init(int water = 0, int food = 0, int preserved = 0, int wood = 0, int components = 0, int parts = 0, bool isVisited = false)
	{
		this.water = water;
		this.food = food;
		this.preserved = preserved;
		this.wood = wood;
		this.components = components;
		this.parts = parts;
		this.isVisited = isVisited;
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
		this.isVisited = isVisited;
		this.landType = landType;
		this.structureType = structureType;
	}

	public void TakeResources(ref int food, ref int preserved, ref int water, ref int wood, ref int components, ref int parts)
	{
		int wt = water = Random.Range(0, this.water + 1);
		int fd = food = Random.Range(0, this.food + 1);
		int ps = preserved = Random.Range(0, this.preserved + 1);
		int wd = wood = Random.Range(0, this.wood + 1);
		int cp = components = Random.Range(0, this.components + 1);
		int pt = parts = Random.Range(0, this.parts + 1);

		GameManager.inst.GetResource(water: wt, food: fd, preserved: ps, wood: wd, components: cp, parts: pt);
		this.water -= wt;
		this.food -= fd;
		this.preserved -= ps;
		this.wood -= wd;
		this.components -= cp;
		this.parts -= pt;
	}

	public void TakeResources(int food = 0, int preserved = 0, int water = 0, int wood = 0, int components = 0, int parts = 0)
	{
		GameManager.inst.GetResource(water, food, preserved, wood, components, parts);
		this.food -= food;
		this.preserved -= preserved;
		this.water -= water;
		this.wood -= wood;
		this.components -= components;
		this.parts -= parts;
	}

	public void OnVisited()
	{
		isVisited = true;
	}
}
