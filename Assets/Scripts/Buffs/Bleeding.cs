using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleeding : Buff
{
	public override bool IsActivated
	{
		get
		{
			return BuffCount > 0;
		}
	}

	public override void Apply(int turn)
	{
		GameManager.inst.HealthDamaged(BuffCount * turn);
	}
}
