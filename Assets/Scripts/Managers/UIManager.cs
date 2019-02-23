using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Linq;

/// <summary>
/// Manager of common UI between scenes
/// </summary>
public class UIManager : SingletonBehaviour<UIManager>
{
	#region Turn UI
	[Header("Turn UI")]
	public Text dayUI;
	public Text timeUI;
	#endregion

	#region Resource UI
	[Header("Resource UI")]
	public Text foodUI;
	public Text preservedUI;
	public Text waterUI;
	public Text woodUI;
	public Text componentsUI;
	public Text partsUI;
	#endregion

	#region Player Status UI
	[Header("Player Status UI")]
	public GameObject playerStatusUI;
	public Slider HealthUI;
	public Slider SanityUI;
	public Slider hungerUI;
	public Slider thirstUI;
	public Slider energyUI;
	#endregion

	#region Turn Passing UI
	[Header("Turn Passing UI")]
	public GameObject turnPassUI;
	public Slider turnPassSlider;
	#endregion

	#region Weather UI
	[Header("Weather UI")]
	public Image weatherUI;
	#endregion

	#region Event Log UI
	[Header("Event Log UI")]
	public GameObject eventLogPanel;
	public Text titleText;
	public Text descriptionText;
	public RectTransform optionGrid;
	public RectTransform resultGrid;
	#endregion

	#region Buff UI
	[Header("Buff UI")]
	public GameObject buffPanel;
	public GameObject buffInfoPanel;
	public Text buffNameText;
	public Text buffDescText;
	public Dictionary<string, BuffUI> buffUIs = new Dictionary<string, BuffUI>();
	#endregion

	#region Cure UI
	[Header("Cure UI")]
	public GameObject curePanel;
	public RectTransform diseaseButtonGrid;
	public RectTransform cureInfoGrid;
	public Button cureButton;
	#endregion

	#region Inventory UI
	[Header("Inventory UI")]
	public GameObject inventoryPanel;
	public GameObject useButton;
	public RectTransform itemButtonGrid;
	public RawImage itemImage;
	public Text itemNameText;
	public Text itemDescText;
	public Text itemAmountText;
	#endregion

	#region Prefabs
	[Header("Prefabs")]
	public GameObject eventButtonPrefab;
	public GameObject resultPrefab;
	public GameObject buffUIPrefab;
	public GameObject diseaseButtonPrefab;
	public GameObject resourceInfoPrefab;
	public GameObject itemButtonPrefab;
	#endregion

	#region System UI
	public GameObject systemPanel;
	private GameObject openedPanel = null;
	public GameObject utilityButtonPanel;

	#endregion

	public GameObject backgroundPanel;

	#region Managers
	private TurnManager tm;
	private GameManager gm;
	#endregion

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (openedPanel != null)
			{
				openedPanel.SetActive(false);
				openedPanel = null;
			}
			else
			{
				OpenSystemPanel();
			}
		}

		for (int i = 0; i <3; ++i)
		{
			if (Input.GetKeyDown(KeyCode.F1 + i))
			{
				utilityButtonPanel.transform.GetChild(i).GetComponent<Button>().onClick.Invoke();
			}
		}

	}

	#region Turn UI Functions
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
	#endregion

	#region Resource UI Functions
	private void UpdateResourcesUI()
	{
		foodUI.text = gm.Food.ToString();
		preservedUI.text = gm.Preserved.ToString();
		waterUI.text = gm.Water.ToString();
		woodUI.text = gm.Wood.ToString();
		componentsUI.text = gm.Components.ToString();
		partsUI.text = gm.Parts.ToString();
	}
	#endregion

	#region Player Status UI Functions
	public void UpdatePlayerStatusUI()
	{
		HealthUI.value = gm.Health / 100f;
		SanityUI.value = gm.Sanity / 100f;
		hungerUI.value = gm.Hunger / 100f;
		thirstUI.value = gm.Thirst / 100f;
		energyUI.value = gm.Energy / 100f;
	}

	public void OpenPlayerStatusUI()
	{
		playerStatusUI.SetActive(true);
	}

	public void ClosePlayerStatusUI()
	{
		playerStatusUI.SetActive(false);
	}
	#endregion

	#region Turn Passing UI Functions
	public void OpenTurnPassUI(int maxTurn, string info)
	{
		turnPassUI.SetActive(true);
		//turnPassInfoText.text = info;
		Vector2 time = tm.Time(maxTurn);
		//turnPassText.text = "00:00 / " + time.x.ToString("00") + ":" + time.y.ToString("00");
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
		//turnPassText.text = curTime.x.ToString("00") + ":" + curTime.y.ToString("00") + " / " + time.x.ToString("00") + ":" + time.y.ToString("00");
		turnPassSlider.value = passedTurn / (float)maxTurn;
	}
	#endregion

	#region Weather UI Functions
	public void UpdateWeatherUI()
	{
		Sprite sprite = Resources.Load<Sprite>("Textures/Weather/" + TurnManager.inst.Weather.ToString());
		weatherUI.sprite = sprite;
	}
	#endregion

	#region Event Log Panel Functions
	public void OpenEventLogPanel(LogEvent logEvent)
	{
		foreach (Transform transform in resultGrid)
		{
			Destroy(transform.gameObject);
		}

		foreach (Transform transform in optionGrid)
		{
			Destroy(transform.gameObject);
		}

		logEvent.EventStart();

		eventLogPanel.SetActive(true);

		titleText.text = logEvent.eventTitle;

		descriptionText.text = logEvent.description;

		List<UnityAction> actions = logEvent.GetActions();

		for (int i = 0; i < actions.Count; ++i)
		{
			GameObject eventButton = Instantiate(eventButtonPrefab, optionGrid);
			eventButton.GetComponent<Button>().onClick.AddListener(actions[i]);
			eventButton.GetComponentInChildren<Text>().text = logEvent.actionDescriptions[i];
		}
	}

	public void AddResourceResult(int food = 0, int preserved = 0, int water = 0, int wood = 0, int components = 0, int parts = 0)
	{
		GameObject result;
		if (food != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Textures/ItemsIcon/Food", food);
		}
		if (preserved != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Textures/ItemsIcon/Preserved", preserved);
		}
		if (water != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Textures/ItemsIcon/Water", water);
		}
		if (wood != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Textures/ItemsIcon/Wood", wood);
		}
		if (components != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Textures/ItemsIcon/Components", components);
		}
		if (parts != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Textures/ItemsIcon/Parts", parts);
		}
	}

	public void AddPlayerStatusResult(int health = 0, int sanity = 0, int hunger = 0, int thirst = 0, int energy = 0)
	{
		GameObject result;
		if (health != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Textures/PlayerStatusIcon/Health", health);
		}
		if (sanity != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Textures/PlayerStatusIcon/Sanity", sanity);
		}
		if (hunger != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Textures/PlayerStatusIcon/Hunger", hunger);
		}
		if (thirst != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Textures/PlayerStatusIcon/Thirst", thirst);
		}
		if (energy != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Textures/PlayerStatusIcon/Energy", energy);
		}
	}

	private void InitResultObject(GameObject result, string path, int amount)
	{
		result.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(path);
		result.GetComponentInChildren<Text>().text = amount.ToString("+#;-#;0");
	}

	public void CloseEventLogPanel()
	{
		eventLogPanel.SetActive(false);

		foreach (Transform transform in resultGrid)
		{
			Destroy(transform.gameObject);
		}
		
		foreach (Transform transform in optionGrid)
		{
			Destroy(transform.gameObject);
		}
	}
	#endregion

	#region MonoBehaviour Functions
	private void Awake()
	{
		gm = GameManager.inst;
		tm = TurnManager.inst;

		tm.OnTurnPassed += UpdateTimeUI;
		tm.OnTurnPassed += UpdateDayUI;
		gm.OnPlayerStatusUpdated += UpdatePlayerStatusUI;
		gm.OnResourceUpdated += UpdateResourcesUI;

		SceneManager.sceneLoaded += OnSceneLoaded;
		DontDestroyOnLoad(gameObject);
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		gameObject.SetActive(scene.name != "Title");
	}
	#endregion

	#region Buff UI Funcitons
	public void UpdateBuffUI(Buff buff)
	{
		if (buff.IsActivated)
		{
			BuffUI buffUI;
			if (!buffUIs.ContainsKey(buff.buffIndexName))
			{
				buffUI = Instantiate(buffUIPrefab, buffPanel.transform).GetComponent<BuffUI>();
				buffUI.transform.Find("IconImage").GetComponent<RawImage>().texture = buff.buffTexture;
				buffUI.Init(buff.buffIndexName, buff.description);
				buffUIs.Add(buff.buffIndexName, buffUI);
			}
			else
			{
				buffUI = buffUIs[buff.buffIndexName];
				buffUI.gameObject.SetActive(true);
			}
			if (buff.buffCount > 0)
				buffUI.transform.Find("BuffCountText").GetComponent<Text>().text = "x" + buff.buffCount.ToString();
			if (buff.remainedTurn > 0)
			{
				Vector2 time = tm.Time(buff.remainedTurn);
				buffUI.transform.Find("BuffTimeText").GetComponent<Text>().text = time.x.ToString("00") + ":" + time.y.ToString("00");
			}
		}
		else if (buffUIs.ContainsKey(buff.buffIndexName))
		{
			buffUIs[buff.buffIndexName].gameObject.SetActive(false);
		}
		
	}

	public void OpenBuffInfoUI(string name, string description)
	{
		buffInfoPanel.SetActive(true);
		buffNameText.text = name;
		buffDescText.text = description;
	}

	public void CloseBuffInfoUI()
	{
		buffInfoPanel.SetActive(false);
	}
	#endregion region

	#region Cure UI Functions
	public void OpenCurePanel()
	{
		if (openedPanel != null)
			openedPanel.SetActive(false);
		openedPanel = curePanel;
		foreach (Transform transform in diseaseButtonGrid)
		{
			Destroy(transform.gameObject);
		}
		foreach (Transform transform in cureInfoGrid)
		{
			Destroy(transform.gameObject);
		}
		curePanel.SetActive(true);
		foreach(var disease in gm.GetDiseases())
		{
			AddDiseaseButton(disease);
		}
	}

	public void CloseCurePanel()
	{
		curePanel.SetActive(false);
	}

	public void AddDiseaseButton(Disease disease)
	{
		GameObject obj = Instantiate(diseaseButtonPrefab, diseaseButtonGrid);
		obj.transform.Find("Image").GetComponent<RawImage>().texture = disease.buffTexture;
		obj.GetComponent<Button>().onClick.AddListener(delegate { ChangeDisease(disease); });
	}

	public void ChangeDisease(Disease disease)
	{
		foreach (Transform transform in cureInfoGrid)
		{
			Destroy(transform.gameObject);
		}
		cureButton.interactable = true;
		cureButton.onClick.RemoveAllListeners();
		cureButton.onClick.AddListener(delegate {
			disease.Cure();
			OpenCurePanel();
			UpdateBuffUI(disease);
			cureButton.onClick.RemoveAllListeners();
		});
		foreach (var str in disease.cureInfoString.Split(' ', '\n'))
		{
			int curResource = 0;
			GameObject obj = Instantiate(resourceInfoPrefab, cureInfoGrid);
			string path = "Textures/";
			string[] info = str.Split(',');

			curResource = GameManager.inst.items[info[0]].amount;
			path += ("Items Icon/" + info[0]);
			obj.transform.Find("RawImage").GetComponent<RawImage>().texture = Resources.Load<Texture2D>(path);
			obj.GetComponentInChildren<Text>().text = curResource + "/" + info[1];
			if (curResource < int.Parse(info[1]))
				cureButton.interactable = false;

			cureButton.onClick.AddListener(delegate {
				gm.items[info[0]].amount -= int.Parse(info[1]);
			});
		}
		cureButton.onClick.AddListener(delegate {
			cureButton.onClick.RemoveAllListeners();
		});
	}
	#endregion

	#region Inventory UI Functions
	public void OpenInventoryPanel()
	{
		if (openedPanel != null)
			openedPanel.SetActive(false);
		openedPanel = inventoryPanel;
		inventoryPanel.SetActive(true);
		foreach (Transform transform in itemButtonGrid.transform)
		{
			Destroy(transform.gameObject);
		}
		ChangeItem(null);

		foreach (var item in gm.items.Values)
		{
			if (item.amount <= 0)
				continue;
			if (item.GetType().IsSubclassOf(typeof(Food)) || item.GetType() == typeof(Food))
			{
				for (int i = 0; i < item.amount; ++i)
				{
					Food food = (Food)item;
					GameObject obj = Instantiate(itemButtonPrefab, itemButtonGrid);
					Debug.Log(i);
					int remainedTurn = food.remainedTurns[i];
					obj.GetComponent<Button>().onClick.AddListener(delegate { ChangeItem(food, remainedTurn); });
					Vector2 time = TurnManager.inst.Time(food.remainedTurns[i]);
					obj.GetComponentInChildren<Text>().text = time.x.ToString("00") + ":" + time.y.ToString("00"); 
					obj.transform.Find("ItemImage").GetComponent<RawImage>().texture = food.itemImage;
				}
			}
			else
			{
				GameObject obj = Instantiate(itemButtonPrefab, itemButtonGrid);
				obj.GetComponent<Button>().onClick.AddListener(delegate { ChangeItem(item); });
				obj.GetComponentInChildren<Text>().text = item.amount.ToString();
				obj.transform.Find("ItemImage").GetComponent<RawImage>().texture = item.itemImage;
			}
		}
	}

	public void CloseInventoryPanel()
	{
		inventoryPanel.SetActive(false);
	}

	public void ChangeItem(Item item)
	{
		if (item == null)
		{
			itemNameText.text = "";
			itemAmountText.text = "";
			itemDescText.text = "";
			itemImage.color = Color.clear;
			useButton.SetActive(false);
			return;
		}

		itemImage.color = Color.white;
		itemNameText.text = item.itemName;
		itemAmountText.text = item.amount.ToString();
		itemDescText.text = item.description;
		itemImage.texture = item.itemImage;
		if (item.GetType().GetInterface("IConsumableItem") == typeof(IConsumableItem))
		{
			useButton.SetActive(true);
			useButton.GetComponent<Button>().onClick.RemoveAllListeners();
			useButton.GetComponent<Button>().onClick.AddListener(delegate { ((IConsumableItem)item).Use(); OpenInventoryPanel(); });
		}
		else
		{
			useButton.SetActive(false);
		}
	}
	
	public void ChangeItem(Food item, int remainedTurn)
	{
		ChangeItem(item);
		useButton.GetComponent<Button>().onClick.RemoveAllListeners();
		useButton.GetComponent<Button>().onClick.AddListener(delegate { item.Use(remainedTurn); OpenInventoryPanel(); });
		Vector2 time = TurnManager.inst.Time(remainedTurn);
		itemAmountText.text = time.x.ToString("00") + ":" + time.y.ToString("00");
	}
	#endregion

	#region System UI Functions
	public void OpenSystemPanel()
	{
		if (openedPanel != null)
			openedPanel.SetActive(false);
		openedPanel = systemPanel;
		systemPanel.SetActive(true);
	}
	#endregion
}
