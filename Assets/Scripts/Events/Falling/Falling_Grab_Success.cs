using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

public class Falling_Grab_Success : LogEvent
{
	public override void EventStart()
	{
		UIManager.inst.AddBuffResult("Bleeding", 2);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction>
		{
			Confirm
		};
	}

	public void Confirm()
	{
		GameManager.inst.GetBuff("Bleeding").buffCount += 2;
	}
}