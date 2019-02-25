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

	#region Bucket UI Variables
	[Header("Bucket UI")]
	public GameObject bucketPanel;
	public Text bucketWaterText;
	public Slider bucketWaterSlider;
	public Button bucketHarvestButton;
	#endregion

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
	public void OpenUpgradePanel(string furnitureName)
	{
		upgradePanel.SetActive(true);
		UpdateUpgradePanel(gm.furnitures[furnitureName]);
	}

	public void CloseUpgradePanel()
	{
		upgradePanel.SetActive(false);
	}

	private void UpdateUpgradePanel(Furniture furniture)
	{
		int level = furniture.level;
		FurnitureUpgradeInfo info = JsonHelper.LoadFurnitureUpgradeInfo(furniture);

		furnitureText.text = furniture.furnitureName;

        if (level >= 3) return;

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
		/*
		if (!GameManager.inst.CheckResource(wood: wood, components: components, parts: parts))
			return;
        if (furniture.type != FurnitureType.Craft && furniture.level >= GameManager.inst.furnitures[(int)FurnitureType.Craft].level)
            return;
		GameManager.inst.UseResource(wood: wood, components: components, parts: parts);
		CloseUpgradePanel();
		GameManager.inst.StartTask(furniture.Upgrade, 4);
		*/
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

	#region Bucket UI Functions
	public void OpenBucketPanel()
	{
		bucketPanel.SetActive(true);
		Bucket bucket = (Bucket)gm.furnitures["Bucket"];
		bucketHarvestButton.interactable = bucket.water > 0;
		bucketWaterText.text = bucket.water.ToString() + "/" + bucket.MaxCapacity.ToString();
		bucketWaterSlider.value = bucket.water / (float)bucket.MaxCapacity;
	}

	public void CloseBucketPanel()
	{
		bucketPanel.SetActive(false);
	}

	public void Bucket_HarvestWater()
	{
		Bucket bucket = (Bucket)gm.furnitures["Bucket"];
		bucket.HarvestWater();
		OpenBucketPanel();
	}
	#endregion
}
