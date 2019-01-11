using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Furniture
{
	public override void UseFurniture()
	{
		
	}

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		throw new System.NotImplementedException();
	}
}
