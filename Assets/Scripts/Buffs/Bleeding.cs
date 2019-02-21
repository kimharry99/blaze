using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleeding : Disease
{
	public override bool IsActivated
	{
		get
		{
			return buffCount > 0;
		}
	}

	public override void Apply(int turn)
	{
		GameManager.inst.healthChangePerTurn -= buffCount;
	}

	public override void Cure()
	{
		buffCount--;
	}
}
