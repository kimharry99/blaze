using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Falling : LogEvent
{
	public override void EventStart()
	{
		
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction>
		{
			Falling_Grab,
			Falling_Landing
		};
	}
	

	public void Falling_Grab()
	{
		if (GameManager.inst.Energy >= 50)
		{
			NextEvent("Falling_Grab_Success");
		}
		else
		{
			NextEvent("Falling_Grab_Fail");
		}
	}

	public void Falling_Landing()
	{
		float rand = Random.Range(0, 100);
		if (rand < 70)
		{
			NextEvent("Falling_Landing_Success");
		}
		else
		{
			NextEvent("Falling_Landing_Fail");
		}
	}
}
