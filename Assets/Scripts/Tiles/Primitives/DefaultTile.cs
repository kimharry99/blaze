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
