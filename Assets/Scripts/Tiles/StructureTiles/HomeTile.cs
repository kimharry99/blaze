using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HomeTile : StructureTile
{
	public override StructureType Type { get { return StructureType.Home; } }

	public override void OnVisited()
	{
		throw new System.NotImplementedException();
	}

	protected override void OpenOptions()
	{
		throw new System.NotImplementedException();
	}
}
