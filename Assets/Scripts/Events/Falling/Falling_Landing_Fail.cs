using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Falling_Landing_Fail : LogEvent
{
	public int health = -10;
	public int bleedCount = 2;

	public override void EventStart()
	{
		UIManager.inst.AddPlayerStatusResult(health: health);
		UIManager.inst.AddBuffResult("Bleeding", bleedCount);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	private void Confirm()
	{
		for (int i = 0; i < bleedCount; ++i)
			((Disease)GameManager.inst.GetBuff("Bleeding")).AddNewDisease();
		GameManager.inst.ChangeHealth(health);
		EndEvent();
	}
}
