using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUIManager : UIManager
{
	private GameManager gm;

	public Text DayUI;
	public Text TimeUI;

	public Button UseFurnitureBtn;

	public GameObject playerStatusUI_Opened;
	public GameObject playerStatusUI_Closed;

	public RectTransform[] furnitureUIs = new RectTransform[10];

	public override void Init()
	{
		gm = GameManager.inst;
		gm.OnTurnPassed += UpdateTimeUI;
		gm.OnTurnPassed += UpdateDayUI;
		//TODO : assign to event handlers
	}

	public override void OnDestroy()
	{
		//TODO : unassign from event handlers
	}

	private void UpdateTimeUI(int turn)
	{
		Vector2 time = GameManager.inst.Time();
		int hour = (int)time.x;
		int minute = (int)time.y;
		TimeUI.text = hour.ToString("00") + ":" + minute.ToString("00");
	}

	private void UpdateDayUI(int turn)
	{
		//TODO : Get day from GameManager, update DayUI
	}

	private void UpdateResourcesUI(object obj, EventArgs e)
	{

	}

	/// <summary>
	/// Make player status UI open or close
	/// </summary>
	/// <param name="isOpen">true = open player status UI, false = close player status UI</param>
	public void OpenPlayerStatusUI(bool isOpen)
	{
		
	}

	public void OpenFurnitureUI(FurnitureType type)
	{
		//TODO : Setactive true form furnitureUIs
	}

	public void CloseFurnitureUI(FurnitureType type)
	{

	}
}
