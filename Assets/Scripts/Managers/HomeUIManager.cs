using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUIManager : SingletonBehaviour<HomeUIManager>
{
	private GameManager gm;
	private TurnManager tm;

	public Text dayUI;
	public Text timeUI;

	public Button UseFurnitureBtn;

	public Text foodUI;
	public Text preservedUI;
	public Text waterUI;
	public Text woodUI;
	public Text componentsUI;
	public Text partsUI;

	public GameObject playerStatusUI;
	public Slider hungerUI;
	public Slider thirstUI;
	public Slider energyUI;

	public GameObject[] furnitureUIs = new GameObject[10];

	public GameObject turnPassUI;
	public Slider turnPassSlider;
	public Text turnPassInfoText;
	public Text turnPassText;

	#region Upgrade Panel
	public GameObject upgradePanel;
	public Text woodText;
	public Text componentsText;
	public Text partsText;
	public Text furnitureText;
	public Button upgradeButton;
	#endregion

	private void Awake()
	{
		gm = GameManager.inst;
		tm = TurnManager.inst;
		tm.OnTurnPassed += UpdateTimeUI;
		tm.OnTurnPassed += UpdateDayUI;
		gm.OnPlayerStatusUpdated += UpdatePlayerStatusUI;
		gm.OnResourceUpdated += UpdateResourcesUI;
		//TODO : assign to event handlers
	}

	private void OnDestroy()
	{
		tm.OnTurnPassed -= UpdateTimeUI;
		tm.OnTurnPassed -= UpdateDayUI;
		gm.OnPlayerStatusUpdated -= UpdatePlayerStatusUI;
		//TODO : unassign from event handlers
	}

	private void UpdateTimeUI(int turn)
	{
		Vector2 time = tm.Time();
		int hour = (int)time.x;
		int minute = (int)time.y;
		timeUI.text = hour.ToString("00") + ":" + minute.ToString("00");
	}

	private void UpdateDayUI(int turn)
	{
		dayUI.text = "Day " + tm.Day.ToString();
	}

	private void UpdateResourcesUI()
	{
		foodUI.text = gm.Food.ToString();
		preservedUI.text = gm.Preserved.ToString();
		waterUI.text = gm.Water.ToString();
		woodUI.text = gm.Wood.ToString();
		componentsUI.text = gm.Components.ToString();
		partsUI.text = gm.Parts.ToString();
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
		furnitureUIs[(int)type].SetActive(true);
	}

	public void CloseFurnitureUI(FurnitureType type)
	{
		furnitureUIs[(int)type].SetActive(false);
	}

	public void UpdatePlayerStatusUI()
	{
		hungerUI.value = gm.Hunger / 100f;
		thirstUI.value = gm.Thirst / 100f;
		energyUI.value = gm.Energy / 100f;
	}

	#region TurnPassUI Functions
	public void OpenTurnPassUI(int maxTurn, string info)
	{
		turnPassUI.SetActive(true);
		turnPassInfoText.text = info;
		Vector2 time = tm.Time(maxTurn);
		turnPassText.text = "00:00 / " + time.x.ToString("00") + ":" + time.y.ToString("00");
		turnPassSlider.value = 0;
	}

	public void CloseTurnPassUI()
	{
		turnPassUI.SetActive(false);
	}

	public void UpdateTurnPassUI(int passedTurn, int maxTurn)
	{
		Vector2 time = tm.Time(maxTurn);
		Vector2 curTime = tm.Time(passedTurn);
		turnPassText.text = curTime.x.ToString("00") + ":" + curTime.y.ToString("00") + " / " + time.x.ToString("00") + ":" + time.y.ToString("00");
		turnPassSlider.value = passedTurn / (float)maxTurn;
	}
	#endregion

	#region Upgrade Panel
	public void OpenUpgradePanel(Furniture furniture)
	{
		upgradePanel.SetActive(true);
		UpdateUpgradePanel(furniture);
	}

	public void CloseUpgradePanel()
	{
		upgradePanel.SetActive(false);
	}

	private void UpdateUpgradePanel(Furniture furniture)
	{
		int level = furniture.Level;
		FurnitureUpgradeInfo info = JsonHelper.LoadFurnitureUpgradeInfo(furniture.type);

		furnitureText.text = furniture.type.ToString();

		woodText.text = "x " + info.wood[level].ToString();
		componentsText.text = "x " + info.components[level].ToString();
		partsText.text = "x " + info.parts[level].ToString();

		upgradeButton.onClick.RemoveAllListeners();
		upgradeButton.onClick.AddListener(
			delegate
			{
				OnUpgradeButtonClicked(info.wood[level], info.components[level], info.parts[level], furniture);
			}
		);
	}

	private void OnUpgradeButtonClicked(int wood, int components, int parts, Furniture furniture)
	{
		Debug.Log("ABC");
		if (!GameManager.inst.CheckResource(wood: wood, components: components, parts: parts))
			return;
		GameManager.inst.UseResource(wood: wood, components: components, parts: parts);
		CloseUpgradePanel();
		GameManager.inst.StartTask(furniture.Upgrade, 4);
	}
	#endregion
}
