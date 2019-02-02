using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainTile : LandTile
{
	public override int MoveCost { get { return 8; } }

	public override LandType type { get { return LandType.Mountain; } }

	public override void OnVisited()
	{
		base.OnVisited();
	}
}
