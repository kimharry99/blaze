using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : Furniture
{
	protected override FurnitureType type { get { return FurnitureType.Kitchen; } }
	// Start is called before the first frame update
	void Start()
	{

	}
	public void UseFurniture()
	{
		throw new System.NotImplementedException();
	}

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		throw new System.NotImplementedException();
	}

	public override void OpenFurnitureUI()
	{
		base.OpenFurnitureUI();
	}

	public override void OnUseButtonClicked()
	{
		throw new System.NotImplementedException();
	}
}