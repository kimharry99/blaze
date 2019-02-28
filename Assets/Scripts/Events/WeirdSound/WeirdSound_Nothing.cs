using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeirdSound_Nothing : LogEvent
{
	public int sanity = -5;
	public override void EventStart()
	{
		UIManager.inst.AddPlayerStatusResult(sanity: sanity);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	public void Confirm()
	{
		GameManager.inst.ChangeSanity(sanity);
		EndEvent();
	}
}
