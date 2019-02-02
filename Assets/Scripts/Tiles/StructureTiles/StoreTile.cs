using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreTile : StructureTile
{
	public override StructureType Type { get { return StructureType.Store; } }

	public override void OnVisited()
	{
		throw new System.NotImplementedException();
	}

	protected override void OpenOptions()
	{
		throw new System.NotImplementedException();
	}
}
