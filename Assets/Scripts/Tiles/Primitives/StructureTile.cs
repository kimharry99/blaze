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
}
