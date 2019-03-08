using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despression : Buff
{
	public override bool IsActivated
	{
		get
		{
			return GameManager.inst.Sanity <= 50 && GameManager.inst.Sanity > 25;
		}
	}

	public override void Apply(int turn)
	{
		GameManager.inst.statusRecoverConst -= 0.25f;
	}
}
