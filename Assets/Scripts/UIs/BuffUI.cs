using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BuffUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField]
	private string buffName, buffDescription;

	public void Init(string name, string desc)
	{
		buffName = name;
		buffDescription = desc;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		UIManager.inst.OpenBuffInfoUI(buffName, buffDescription);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		UIManager.inst.CloseBuffInfoUI();
	}
}
