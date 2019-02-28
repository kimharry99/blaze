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
		UIManager.inst.AddPlayerStatusResult(GameManager.inst.MaxHunger, GameManager.inst.MaxThirst, GameManager.inst.MaxEnergy);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	private void Confirm()
	{
		GameManager.inst.GetResource(amount, amount, amount, amount, amount, amount);
		GameManager.inst.ChangeHunger(GameManager.inst.MaxHunger);
		GameManager.inst.ChangeThirst(GameManager.inst.MaxThirst);
		GameManager.inst.ChangeEnergy(GameManager.inst.MaxEnergy);
		EndEvent();
	}
}
