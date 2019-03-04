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

    #region Farm UI Variables
    [Header("Farm UI")]
    public GameObject farmPanel;
    public GameObject cropSelectPanels;
    public Button[] plantCropButtons = new Button[4];
    public Sprite[] cropImages = new Sprite[4];
    public Image[] slotImages = new Image[3];
    public Image[] fillImages = new Image[3];
    public Text[] farmTexts = new Text[3];
    public Slider[] farmSliders = new Slider[3];
    public Button[] farmCropSelectButtons = new Button[3];
    public Button[] farmHarvestButtons = new Button[3];
    public Button[] farmCancelButtons = new Button[3];
    #endregion

    #region Bed UI Variables
    [Header("Bed UI")]
    public GameObject bedPanel;
    public Button bedTurnPlusButton;
    public Button bedTurnMinusButton;
    public Text bedUsingTurnText;
    #endregion

    #region Door UI Variables
    [Header("Door UI")]
    public GameObject doorPanel;
    public Button doorUseButton;
    #endregion

    #region Kitchen UI Variables
    [Header("Kitchen UI")]
    public GameObject kitchenPanel;
    public Button[] kitchenButtons = new Button[7];
    public int kitchenOption;
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
		if (!GameManager.inst.CheckResource(wood: wood, components: components, parts: parts))
			return;
        if (furniture.name != "Craft" && furniture.level >= GameManager.inst.furnitures["Craft"].level)
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

    #region Farm UI Functions
    public void OpenFarmPanel()
    {
        Farm farm = (Farm)gm.furnitures["Farm"];
        for(int i = 0; i < 3; i++)
        {
            if (i < farm.level)
            {
                farmHarvestButtons[i].interactable = farm.state[i] == 2;    //Grow
                farmCancelButtons[i].interactable = farm.state[i] != 0;     //Idle
                farmCropSelectButtons[i].interactable = farm.state[i] == 0; //Idle
                if (farm.state[i] == 0)
                {
                    farmTexts[i].text = "0/0";
                    farmSliders[i].value = 0;
                }
                else if (farm.state[i] == 1)
                {
                    farmTexts[i].text = farm.turnLeft[i].ToString() + "/" + farm.turnTaken[i].ToString();
                    farmSliders[i].value = farm.turnLeft[i] / (float)farm.turnTaken[i];
                }
                else
                {
                    farmTexts[i].text= (-1*farm.turnLeft[i]).ToString() + "/" + 196.ToString();
                    farmSliders[i].value = 0;
                }
            }
        }
        farmPanel.SetActive(true);
    }

    public void CloseFarmPanel()
    {
        Debug.Log("click");
        farmPanel.SetActive(false);
    }

    public void OpenCropSelectPanel(int slot)
    {
        if (!cropSelectPanels.activeSelf)
        {
            Farm farm = (Farm)gm.furnitures["Farm"];
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0: //상추
                        plantCropButtons[i].interactable = GameManager.inst.CheckResource(water: 20, food: 5)&& i <= farm.level; 
                        break;
                    case 1: //당근
                        plantCropButtons[i].interactable = GameManager.inst.CheckResource(water: 10, food: 20) && i <= farm.level;
                        break;
                    case 2: //콩
                        plantCropButtons[i].interactable = GameManager.inst.CheckResource(water: 20, food: 10) && i <= farm.level;
                        break;
                    case 3: //감자
                        plantCropButtons[i].interactable = GameManager.inst.CheckResource(water: 40, food: 50) && i <= farm.level;
                        break;
                    default:
                        break;
                }
            }
            cropSelectPanels.GetComponent<RectTransform>().localPosition = new Vector3(-150 + slot * 450,0, 0);

            farm.selectedSlot = slot;
            cropSelectPanels.SetActive(true);
        }
    }

    public void CloseCropSelectPanel(int slot)
    {
        Debug.Log("click");
        Farm farm = (Farm)gm.furnitures["Farm"];
        farm.selectedSlot = -1;
        cropSelectPanels.SetActive(false);
    }

    
    public void Farm_PlantCrop(int crop)
    {
        Debug.Log("click");
        tm.UseTurn(8);
        Farm farm = (Farm)gm.furnitures["Farm"];
        slotImages[farm.selectedSlot].GetComponent<Image>().sprite = cropImages[crop];
        fillImages[farm.selectedSlot].GetComponent<Image>().sprite = cropImages[crop];
        farm.PlantCrops(crop);
        CloseCropSelectPanel(farm.selectedSlot);
        CloseFarmPanel();
    }

    public void Farm_HarvestCrop(int slot)
    {
        Farm farm = (Farm)gm.furnitures["Farm"];
        farm.selectedSlot = slot;
        tm.UseTurn(4);
        slotImages[farm.selectedSlot].GetComponent<Image>().sprite = null;
        farm.HarvestCrops(farm.crops[farm.selectedSlot]);
        OpenFarmPanel();
    }

    public void Farm_CancelSlot(int slot)
    {
        Farm farm = (Farm)gm.furnitures["Farm"];
        farm.selectedSlot = slot;
        slotImages[farm.selectedSlot].GetComponent<Image>().sprite = null;
        farm.CancelCrops();
        OpenFarmPanel();
    }
    #endregion

    #region Bed UI Functions
    public void OpenBedPanel()
    {
        bedPanel.SetActive(true);
        Bed bed = (Bed)gm.furnitures["Bed"];
        bedTurnMinusButton.interactable = bed.usingTurn > 0;
        bedUsingTurnText.text = bed.usingTurn.ToString();
    }

    public void CloseBedPanel()
    {
        bedPanel.SetActive(false);
    }

    public void Bed_PlusTurn()
    {
        Bed bed = (Bed)gm.furnitures["Bed"];
        bed.PlusTurn();
        OpenBedPanel();
    }

    public void Bed_MinusTurn()
    {
        Bed bed = (Bed)gm.furnitures["Bed"];
        bed.MinusTurn();
        OpenBedPanel();
    }

    public void Bed_UseBed()
    {
        Bed bed = (Bed)gm.furnitures["Bed"];
        bed.UseBed();
        OpenBedPanel();
    }
    #endregion

    #region Door UI Functions
    public void OpenDoorPanel()
    {
        doorPanel.SetActive(true);
    }

    public void CloseDoorPanel()
    {
        doorPanel.SetActive(false);
    }

    public void Door_GoOutside()
    {
        Door door = (Door)gm.furnitures["Door"];
        door.GoOutside();
        CloseDoorPanel();
    }
    #endregion

    #region Kitchen UI Functions
    public void SelectRecipie(int recipie)
    {
        Kitchen kitchen = (Kitchen)gm.furnitures["Kitchen"];
        kitchen.SelectRecipie(recipie);
    }
    #endregion
}
