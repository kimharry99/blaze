using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fire_RunAway : LogEvent
{
	public int minResource = 0, maxResource = 5;
	private int resource;

	public override void EventStart()
	{
		resource = -Random.Range(minResource, maxResource + 1);
		UIManager.inst.AddResourceResult(resource, resource, resource, resource, resource, resource);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	private void Confirm()
	{
		MapManager mm = MapManager.inst;
		mm.structureTilemap.SetTile(mm.curPosition, mm.structureTiles[(int)StructureType.None]);
		GameManager.inst.GetResource(resource, resource, resource, resource, resource, resource);
		EndEvent();
	}
}
