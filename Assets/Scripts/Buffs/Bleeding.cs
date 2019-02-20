using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleeding : Disease
{
	public override bool IsActivated
	{
		get
		{
			return BuffCount > 0;
		}
	}

	public override bool IsCureable
	{
		get
		{
			return true;
		}
	}

	public override void Apply(int turn)
	{
		GameManager.inst.healthChangePerTurn -= BuffCount;
	}

	public override void Cure()
	{
		throw new System.NotImplementedException();
	}
}
