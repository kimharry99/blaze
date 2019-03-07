using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HospitalTile : StructureTile
{
	public override int RestAmount { get { return 25; } }

	public override List<UnityAction> GetTileActions()
	{
		List<UnityAction> actions = base.GetTileActions();
		if (!MapManager.inst.GetCurrentTileInfo().isHarvested)
			actions.Add(MakeMedicine);
		return actions;
	}

	private void MakeMedicine()
	{
		EventManager.inst.StartEvent("ManufactureMedicine");
	}
}
