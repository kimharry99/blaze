using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncorruptibleFood : Food
{
	public override void Init()
	{
		amount = 0;
	}

	public override void AddNewFood()
	{
		amount++;
	}

	public override void AddNewFood(int remainedTurn)
	{
		AddNewFood();
	}
}
