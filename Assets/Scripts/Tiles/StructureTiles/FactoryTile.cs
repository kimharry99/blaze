using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FactoryTile : StructureTile
{
	public override StructureType Type { get { return StructureType.Factory; } }

	public override int RestAmount { get { return 20; } }

	public override void OnVisited()
	{
		throw new System.NotImplementedException();
	}

	public override List<UnityAction> GetTileActions()
	{
		List<UnityAction> actions = base.GetTileActions();
		return actions;
	}
}
