using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

public class Falling_Grab_Success : LogEvent
{
	public int bleedCount = 2;
	public override void EventStart()
	{
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
		EndEvent();
	}
}