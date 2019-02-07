using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FieldTile : LandTile
{
	public override int MoveCost { get { return 2; } }

	public override LandType type { get { return LandType.Field; } }

	public override void OnVisited(Vector3Int pos)
	{
		base.OnVisited(pos);
	}
}
