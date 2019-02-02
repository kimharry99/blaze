using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestTile : StructureTile
{
	public override StructureType Type { get { return StructureType.Forest; } }

	public override void OnVisited()
	{
		throw new System.NotImplementedException();
	}

	protected override void OpenOptions()
	{
		throw new System.NotImplementedException();
	}
}
