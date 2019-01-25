using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class DefaultTile : Tile
{
	#region Resources
	public int Water { get; private set; }
	public int Food { get; private set; }
	public int Preserved { get; private set; }
	public int Wood { get; private set; }
	public int Components { get; private set; }
	public int Parts { get; private set; }
	#endregion


	public void Init(int water, int food, int preserved, int wood, int components, int parts)
	{

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

	public abstract void OnVisited();
}
