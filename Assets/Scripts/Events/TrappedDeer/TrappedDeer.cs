using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrappedDeer : LogEvent
{
	public override void EventStart()
	{
		
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction>
		{
			TrappedDeer_Release,
			TrappedDeer_Kill,
			TrappedDeer_No
		};
	}

	private void TrappedDeer_Release()
	{
		float rand = Random.Range(0f, 100f);
		if (rand < 40)
		{
			NextEvent("TrappedDeer_RunAway");
		}
		else
		{
			NextEvent("TrappedDeer_Parts");
		}
	}

	private void TrappedDeer_Kill()
	{
		float rand = Random.Range(0f, 100f);
		if (rand < 80)
		{
			NextEvent("TrappedDeer_Reality");
		}
		else
		{
			NextEvent("TrappedDeer_Resist");
		}
	}

	private void TrappedDeer_No()
	{
		NextEvent("TrappedDeer_Ignore");
	}
}
