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
		if (EventSystem.current.IsPointerOverGameObject())
			return;
		Debug.Log(furnitureName);
		Furniture furniture = GameManager.inst.furnitures[furnitureName];
		if (furniture.level <= 3)
			OnClicked.Invoke();
	}
}
