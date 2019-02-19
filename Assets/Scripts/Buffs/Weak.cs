using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weak : Buff
{
	public override bool IsActivated {
		get
		{
			return GameManager.inst.Health <= 50 && GameManager.inst.Health > 25;
		}
	}

	public override void Apply(int turn)
	{
		//TODO
	}
}
