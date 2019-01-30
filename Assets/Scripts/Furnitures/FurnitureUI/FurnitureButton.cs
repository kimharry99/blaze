using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FurnitureButton : MonoBehaviour
{
	private void OnMouseUp()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;
		Furniture furniture = GetComponentInParent<Furniture>();
		HomeUIManager.inst.OpenFurnitureUI(furniture.type);
	}
}
