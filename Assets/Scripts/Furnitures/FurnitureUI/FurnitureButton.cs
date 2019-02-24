using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FurnitureButton : MonoBehaviour
{
	public UnityEvent OnClicked;
	public string furnitureName;
	private void OnMouseUp()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;
		OnClicked.Invoke();
	}
}
