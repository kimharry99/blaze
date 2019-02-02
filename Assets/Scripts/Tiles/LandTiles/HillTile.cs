using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HillTile : LandTile
{
	public override int MoveCost { get { return 4; } }

	public override LandType type { get { return LandType.Hill; } }

	public override void OnVisited()
	{
		base.OnVisited();
	}
}
