using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUIManager : UIManager
{
	private GameManager gm;

	public Text dayUI;
	public Text timeUI;

	public Button UseFurnitureBtn;

	public GameObject playerStatusUI;
	public Slider hungerUI;
	public Slider thirstUI;
	public Slider energyUI;

	public RectTransform[] furnitureUIs = new RectTransform[10];

	public override void Init()
	{
		gm = GameManager.inst;
		gm.OnTurnPassed += UpdateTimeUI;
		gm.OnTurnPassed += UpdateDayUI;
		gm.OnPlayerStatusUpdated += UpdatePlayerStatusUI;
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
		timeUI.text = hour.ToString("00") + ":" + minute.ToString("00");
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
	/// <param name="open">true = open player status UI, false = close player status UI</param>
	public void TurnPlayerStatusUI(bool open)
	{
		playerStatusUI.SetActive(open);
	}

	public void OpenFurnitureUI(FurnitureType type)
	{
		//TODO : Setactive true form furnitureUIs
	}

	public void CloseFurnitureUI(FurnitureType type)
	{

	}

	public void UpdatePlayerStatusUI()
	{
		hungerUI.value = gm.Hunger / 100f;
		thirstUI.value = gm.Thirst / 100f;
		energyUI.value = gm.Energy / 100f;
	}
}
