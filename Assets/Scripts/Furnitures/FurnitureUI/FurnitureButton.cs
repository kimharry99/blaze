using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureButton : MonoBehaviour
{
	private void OnMouseDown()
	{
		Furniture furniture = GetComponentInParent<Furniture>();
		HomeUIManager.inst.OpenFurnitureUI(furniture.type);
	}
}
