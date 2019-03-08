using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UpgradeButton : MonoBehaviour
{
	public UnityEvent OnClicked;
	public string furnitureName;
	private void OnMouseUp()
	{
		/*
		if (EventSystem.current.IsPointerOverGameObject())
			return;
		*/
		Furniture furniture = GameManager.inst.furnitures[furnitureName];
		if (furniture.Level <= 2)
			OnClicked.Invoke();
	}
}
