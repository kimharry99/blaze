using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NoneTile : StructureTile
{
	public override StructureType Type { get { return StructureType.None; } }

	public override int RestAmount { get { return 10; } }

	public override List<UnityAction> GetTileActions()
	{
		List<UnityAction> actions = base.GetTileActions();
		return actions;
	}

	public override void OnVisited()
	{
		throw new System.NotImplementedException();
	}
}
