using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParkTile : StructureTile
{
	public override int RestAmount { get { return 50; }}

	public override List<UnityAction> GetTileActions()
	{
		return base.GetTileActions();
	}

	public override void OnVisited(Vector3Int pos)
	{
		base.OnVisited(pos);
	}
}
