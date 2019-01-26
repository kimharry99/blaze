using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manager of UIs that are only used in Home Scene
/// </summary>
public class HomeUIManager : SingletonBehaviour<HomeUIManager>
{
	public GameObject[] furnitureUIs = new GameObject[10];

	#region Upgrade UI
	public GameObject upgradePanel;
	public Text woodText;
	public Text componentsText;
	public Text partsText;
	public Text furnitureText;
	public Button upgradeButton;
	#endregion

	#region Managers
	private GameManager gm;
	private TurnManager tm;
	#endregion

	#region Furniture UI Functions
	public void OpenFurnitureUI(FurnitureType type)
	{
		furnitureUIs[(int)type].SetActive(true);
	}

	public void CloseFurnitureUI(FurnitureType type)
	{
		furnitureUIs[(int)type].SetActive(false);
	}
	#endregion

	#region Upgrade UI Functions
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

	private void Awake()
	{
		gm = GameManager.inst;
		tm = TurnManager.inst;
	}

	public void ClosePanel(GameObject obj)
	{
		obj.SetActive(false);
	}
}
