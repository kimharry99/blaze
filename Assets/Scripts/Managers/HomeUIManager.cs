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

	#region Generator UI Variables
	[Header("Generator UI")]
	public GameObject generatorPanel;
	public Button generatorUseButton;
	public Button[] generatorOptionButtons;

	public GameObject generatorChargePanel;
	public Button generatorHarvestButton;
	public Slider generatorRemainedTurnSlider;
	public Text generatorRemainedTimeText;
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

	#region Generator UI Functions
	public void OpenGeneratorPanel()
	{
		Debug.Log("Generator");
		Generator generator = (Generator)GameManager.inst.furnitures["Generator"];
		if (generator.remainedTurn > 0 || generator.isFinished)
		{
			generatorChargePanel.SetActive(true);
			generatorPanel.SetActive(false);
			generatorHarvestButton.interactable = generator.isFinished;
			generatorRemainedTurnSlider.value = (generator.neededTurn[generator.option] - generator.remainedTurn) / generator.neededTurn[generator.option];
			if (!generator.isFinished) {
				Vector2 time = TurnManager.inst.GetTime(generator.remainedTurn);
				generatorRemainedTimeText.text = time.x.ToString("00") + ":" + time.y.ToString("00");
			}
		}
		else
		{
			generatorChargePanel.SetActive(false);
			generatorPanel.SetActive(true);
			foreach (var btn in generatorOptionButtons)
			{
				btn.interactable = false;
			}
			for (int i = 0; i < GameManager.inst.furnitures["Generator"].level; ++i)
			{
				generatorOptionButtons[i].interactable = true;
			}
		}
	}

	public void CloseGeneratorPanel()
	{
		generatorChargePanel.SetActive(false);
		generatorPanel.SetActive(false);
	}

	public void ChangeGeneratorOption(int option)
	{
		Generator generator = (Generator)GameManager.inst.furnitures["Generator"];
		generatorUseButton.interactable = GameManager.inst.CheckResource(wood: generator.woodNeeded[option], parts: generator.partsNeeded[option]);
	}

	public void OnGeneratorUseButtonClicked()
	{
		Generator generator = (Generator)GameManager.inst.furnitures["Generator"];
		GameManager.inst.StartTask(delegate { generator.Use(); OpenGeneratorPanel(); }, 4);
	}

	public void OnGeneratorHarvestButtonClicked()
	{
		Generator generator = (Generator)GameManager.inst.furnitures["Generator"];
		generator.Harvest();
		OpenGeneratorPanel();
	}
	#endregion
}
