using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftTableObject : FurnitureObject
{
	public override FurnitureType type { get { return FurnitureType.Craft; } }

	public override void OnUseButtonClicked()
	{
		throw new System.NotImplementedException();
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

	// Update is called once per frame
	void Update()
	{

	}
}
