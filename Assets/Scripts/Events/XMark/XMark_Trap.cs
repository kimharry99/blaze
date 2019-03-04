using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class XMark_Trap : LogEvent
{
	public int loseHealthAmount = -30;

	public override void EventStart()
	{
		UIManager.inst.AddPlayerStatusResult(health: loseHealthAmount);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	private void Confirm()
	{
		GameManager.inst.ChangeHealth(loseHealthAmount);
		EndEvent();
	}
}
