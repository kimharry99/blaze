using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class XMark_JackPot : LogEvent
{
	public int amount = 50;

	public override void EventStart()
	{
		UIManager.inst.AddResourceResult(amount, amount, amount, amount, amount, amount);
		UIManager.inst.AddPlayerStatusResult(100, 100, 100);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	private void Confirm()
	{
		GameManager.inst.GetResource(amount, amount, amount, amount, amount, amount);
		GameManager.inst.Eat(100);
		GameManager.inst.Drink(100);
		GameManager.inst.Rest(100);
		EndEvent();
	}
}
