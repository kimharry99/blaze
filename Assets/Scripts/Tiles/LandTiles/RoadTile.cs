using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoadTile : LandTile
{
	public override int MoveCost { get { return 1; } }

	public override LandType type { get { return LandType.Road; } }

	public override void OnVisited(Vector3Int pos)
	{
		base.OnVisited(pos);
	}
}
