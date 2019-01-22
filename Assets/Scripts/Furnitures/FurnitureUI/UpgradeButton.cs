using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
	private void OnMouseDown()
	{
		Furniture furniture = GetComponentInParent<Furniture>();
		if (furniture.Level <= 3)
			HomeUIManager.inst.OpenUpgradePanel(furniture);
	}
}
