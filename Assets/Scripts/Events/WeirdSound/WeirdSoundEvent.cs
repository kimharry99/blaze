using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeirdSoundEvent : LogEvent
{
	public override void EventStart()
	{

	}

	private void WeirdSound_Yes()
	{
		TurnManager.inst.UseTurn(4);

		float rand = Random.Range(0, 10);
		if (rand < 5)
		{
			NextEvent("WeirdSound_Nothing");
		}
		else if (rand < 9)
		{
			NextEvent("WeirdSound_Food");
		}
		else
		{
			NextEvent("WeirdSound_Cat");
		}
	}

	private void WeirdSound_No()
	{
		NextEvent("WeirdSound_Ignore");
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction>
		{
			WeirdSound_Yes,
			WeirdSound_No
		};
	}
}