using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HungryCat_Sad : LogEvent
{
	public int sanity = -10;

	public override void EventStart()
	{
		UIManager.inst.AddPlayerStatusResult(sanity: sanity);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	private void Confirm()
	{
		GameManager.inst.ChangeSanity(sanity);
		EndEvent();
	}
}
