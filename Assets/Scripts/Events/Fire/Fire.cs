using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fire : LogEvent
{
	public override void EventStart()
	{
		
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction>
		{
			Fire_Extinguish,
			Fire_RunAway
		};
	}

	private void Fire_Extinguish()
	{
		float rand = Random.Range(0, 100);
		if (rand < 50)
			NextEvent("Fire_Success");
		else
			NextEvent("Fire_Fail");
	}

	private void Fire_RunAway()
	{
		NextEvent("Fire_RunAway");
	}
}
