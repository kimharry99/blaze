using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoarseMeal : Food
{
	public override void Use()
	{
		base.Use();
		float rand = Random.Range(0f, 100f);
		if (rand < 10)
		{
			((Disease)GameManager.inst.GetBuff("FoodPoisoning")).AddNewDisease();
		}
	}
}
