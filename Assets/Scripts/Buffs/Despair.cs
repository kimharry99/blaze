using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despair : Buff
{
	public override bool IsActivated
	{
		get
		{
			return GameManager.inst.Sanity <= 25;
		}
	}

	public override void Apply(int turn)
	{
		GameManager.inst.statusRecoverConst -= 0.5f;
	}
}
