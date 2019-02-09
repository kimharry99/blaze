using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrappedDeer_Ignore : LogEvent
{

	public override void EventStart()
	{
		
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { EndEvent };
	}
}
