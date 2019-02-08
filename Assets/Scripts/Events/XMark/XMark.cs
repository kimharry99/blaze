using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class XMark : LogEvent
{
	public override void EventStart()
	{
		
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction>
		{
			XMark_Yes,
			XMark_No
		};
	}

	private void XMark_Yes()
	{
		TurnManager.inst.UseTurn(4);

		float rand = Random.Range(0f, 10f);
		if (rand < 2)
		{
			NextEvent("XMark_Box");
		}
		else if (rand < 4)
		{
			NextEvent("XMark_Trap");
		}
		else if (rand < 4.5f)
		{
			NextEvent("XMark_JackPot");
		}
		else
		{
			NextEvent("XMark_Nothing");
		}
	}

	private void XMark_No()
	{
		NextEvent("XMark_Ignore");
	}
}
