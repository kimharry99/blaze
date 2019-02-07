using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeirdSound_Food : LogEvent
{
	private int preserved, water;

	public override void EventStart()
	{
		preserved = Random.Range(10, 21);
		water = Random.Range(5, 11);

		UIManager.inst.AddResourceResult(preserved: preserved, water: water);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	private void Confirm()
	{
		GameManager.inst.GetResource(preserved: preserved, water: water);
		UIManager.inst.CloseEventLogPanel();
	}
}
