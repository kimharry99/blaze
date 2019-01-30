﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeButton : MonoBehaviour
{
	private void OnMouseUp()
	{
		Debug.Log(EventSystem.current.IsPointerOverGameObject());
		if (EventSystem.current.IsPointerOverGameObject())
			return;
		Furniture furniture = GetComponentInParent<Furniture>();
		if (furniture.Level <= 3)
			HomeUIManager.inst.OpenUpgradePanel(furniture);
	}
}
