using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FieldTile : LandTile
{
	public override int MoveCost { get { return 1; } }

	public override void OnVisited()
	{
		base.OnVisited();
	}
}
