using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeirdSound_Cat : LogEvent
{
	private int components, parts;

	public override void EventStart()
	{
		components = Random.Range(10, 21);
		parts = Random.Range(5, 11);

		UIManager.inst.AddResourceResult(components: components, parts: parts);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	private void Confirm()
	{
		GameManager.inst.GetResource(components: components, parts: parts);
		EndEvent();
	}
}
