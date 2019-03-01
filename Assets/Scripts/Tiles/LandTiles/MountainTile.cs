using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainTile : LandTile
{
	public override int MoveCost { get { return 8; } }

	public override LandType type { get { return LandType.Mountain; } }

	public override void OnVisited(Vector3Int pos)
	{
		base.OnVisited(pos);
		float rand = Random.Range(0f, 100f);
		if (rand < 2)
		{
			UIManager.inst.OpenEventLogPanel(EventManager.inst.GetEvent("Falling"));
		}
	}
}
