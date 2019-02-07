using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeirdSoundEvent : LogEvent
{
	public int minPreserved, maxPreserved;
	public int minWater, maxWater;
	public int sanityDamage;

	public override void EventStart()
	{

	}

	private void Yes()
	{
		TurnManager.inst.UseTurn(4);

		float rand = UnityEngine.Random.Range(0, 10);
		if (rand < 5)
		{
			UIManager.inst.OpenEventLogPanel(EventManager.inst.GetEvent("WeirdSound_Nothing"));
		}
		else if (rand < 9)
		{
			UIManager.inst.OpenEventLogPanel(EventManager.inst.GetEvent("WeirdSound_Food"));
		}
		else
		{
			UIManager.inst.OpenEventLogPanel(EventManager.inst.GetEvent("WeirdSound_Cat"));
		}
	}

	private void No()
	{
		UIManager.inst.OpenEventLogPanel(EventManager.inst.GetEvent("WeirdSound_Ignore"));
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction>
		{
			Yes,
			No
		};
	}
}