using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeirdSound_Nothing : LogEvent
{
	public override void EventStart()
	{
		UIManager.inst.AddPlayerStatusResult(sanity: -5);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	public void Confirm()
	{
		GameManager.inst.SanityDamaged(5);
		UIManager.inst.CloseEventLogPanel();
	}
}
