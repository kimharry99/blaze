using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Linq;
using System.Reflection;

/// <summary>
/// Manager of common UI between scenes
/// </summary>
public class UIManager : SingletonBehaviour<UIManager>
{
	#region Turn UI
	[Header("Turn UI")]
	public Text dayUI;
	public Text timeUI;
	public RectTransform minuteHand;
	public RectTransform hourHand;
	public RawImage dayNightImage;
	public Texture2D[] dayNightTextures;
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
	public Text healthPerTurnText;
	public Text sanityPerTurnText;
	public Text hungerPerTurnText;
	public Text thirstPerTurnText;
	public Text energyPerTurnText;
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
	public RawImage eventImage;
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

	#region Character Info UI
	[Header("Character Info UI")]
	public GameObject characterPanel;
	public Text StatusPointUI;
	public Text MaxHealthUI;
	public Text MaxSanityUI;
	public Text MaxHungerUI;
	public Text MaxThirstUI;
	public Text MaxEnergyUI;
	#endregion

	#region Miscellneous UI
	[Header("Miscellnous UI")]
	public GameObject translucentPanel;
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
	[Header("System UI")]
	public GameObject systemPanel;
	private GameObject openedPanel = null;
	public GameObject utilityButtonPanel;

	#endregion

	#region Option UI
	[Header("Option UI")]
	public GameObject optionPanel;
	public Slider turnPassSlider;
	public Text turnPassText;
	#endregion

	public GameObject backgroundPanel;

	#region Managers
	private TurnManager tm;
	private GameManager gm;
	#endregion

	#region GameOver UI
	[Header("GameOver UI")]
	public GameObject GameOverPanel;
	public Image GameOverCharacterImage;
	public Text GameResultText, GameOverText;
	public Button GameOverButton;
	[SerializeField]
	private Sprite dead_health, dead_sanity;
	[SerializeField]
	private AudioClip gameOverSFX;
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

		for (int i = 0; i < 3; ++i)
		{
			if (Input.GetKeyDown(KeyCode.F1 + i))
			{
				utilityButtonPanel.transform.GetChild(i).GetComponent<Button>().onClick.Invoke();
			}
		}

	}

	#region Turn UI Functions
	public void UpdateTimerUI(int turn)
	{
		Vector2 time = tm.GetTime();
		int hour = (int)time.x;
		int minute = (int)time.y;
		hourHand.rotation = Quaternion.Euler(0, 0, -30 * hour - 0.5f * minute);
		minuteHand.rotation = Quaternion.Euler(0, 0, -6 * minute);
		dayUI.text = tm.Day.ToString();
		dayNightImage.texture = dayNightTextures[(int)TurnManager.inst.DayNight + 1];
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
		HealthUI.value = gm.Health / (float)gm.MaxHealth;
		SanityUI.value = gm.Sanity / (float)gm.MaxSanity;
		hungerUI.value = gm.Hunger / (float)gm.MaxHunger;
		thirstUI.value = gm.Thirst / (float)gm.MaxThirst;
		energyUI.value = gm.Energy / (float)gm.MaxEnergy;
    }

	public void UpdateStatusChangePerTurnUI()
	{
		IntegerTextFormatting(healthPerTurnText, gm.healthChangePerTurn, true);
		IntegerTextFormatting(sanityPerTurnText, gm.sanityChangePerTurn, true);
		IntegerTextFormatting(hungerPerTurnText, gm.hungerChangePerTurn, true);
		IntegerTextFormatting(thirstPerTurnText, gm.thirstChangePerTurn, true);
		IntegerTextFormatting(energyPerTurnText, gm.energyChangePerTurn, true);
	}
	#endregion

	/*
    #region Turn Passing UI Functions
	
    public void OpenTurnPassUI(int maxTurn, string info)
	{
		turnPassUI.SetActive(true);
		Vector2 time = tm.GetTime(maxTurn);
		turnPassSlider.value = 0;
	}

	public void CloseTurnPassUI()
	{
		turnPassUI.SetActive(false);
	}

	public void UpdateTurnPassUI(int passedTurn, int maxTurn)
	{
		Vector2 time = tm.GetTime(maxTurn);
		Vector2 curTime = tm.GetTime(passedTurn);
		turnPassSlider.value = passedTurn / (float)maxTurn;
	}
	#endregion
	*/

	#region Miscellneous
	public void OpenTranslucentPanel()
	{
		translucentPanel.SetActive(true);
	}
	public void CloseTranslucentPanel()
	{
		translucentPanel.SetActive(false);
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

		eventImage.texture = logEvent.eventTexture;
	}

	public void AddResourceResult(int food = 0, int preserved = 0, int water = 0, int wood = 0, int components = 0, int parts = 0)
	{
		GameObject result;
		if (food != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Food", food, true);
		}
		if (preserved != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Preserved", preserved, true);
		}
		if (water != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Water", water, true);
		}
		if (wood != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Wood", wood, true);
		}
		if (components != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Components", components, true);
		}
		if (parts != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Parts", parts, true);
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

	public void AddBuffResult(string buffName, int amount = 0)
	{
		GameObject result = Instantiate(resultPrefab, resultGrid);
		result.GetComponentInChildren<RawImage>().texture = gm.GetBuff(buffName).buffTexture;
		IntegerTextFormatting(result.GetComponentInChildren<Text>(), amount, true);
	}

	public void AddItemResult(string itemName, int amount)
	{
		if (amount <= 0)
			return;
		GameObject result = Instantiate(resultPrefab, resultGrid);
		result.GetComponentInChildren<RawImage>().texture = gm.items[itemName].itemImage;
		IntegerTextFormatting(result.GetComponentInChildren<Text>(), amount, true);
	}

	private void InitResultObject(GameObject result, string path, int amount, bool isItem = false)
	{
		if (isItem)
			result.GetComponentInChildren<RawImage>().texture = GameManager.inst.items[path].itemImage;
		else
			result.GetComponentInChildren<RawImage>().texture = Resources.Load<Texture>(path);
		IntegerTextFormatting(result.GetComponentInChildren<Text>(), amount);
		//result.GetComponentInChildren<Text>().text = amount.ToString("+#;-#;0");
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
	protected override void Awake()
	{
		if (inst != this)
		{
			Destroy(gameObject);
			return;
		}

		gm = GameManager.inst;
		tm = TurnManager.inst;

		tm.OnTurnPassed += UpdateTimerUI;
		gm.OnPlayerStatusUpdated += UpdatePlayerStatusUI;
		gm.OnResourceUpdated += UpdateResourcesUI;

		//SceneManager.sceneLoaded += OnSceneLoaded;
		DontDestroyOnLoad(gameObject);
	}
	/*
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		gameObject.SetActive(scene.name != "Title");
	}
	*/
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
				Vector2 time = tm.GetTime(buff.remainedTurn);
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
				if (item.GetType() == typeof(UncorruptibleFood))
				{
					UncorruptibleFood food = (UncorruptibleFood)item;
					GameObject obj = Instantiate(itemButtonPrefab, itemButtonGrid);
					obj.GetComponent<Button>().onClick.AddListener(delegate { ChangeItem(food); });
					obj.GetComponentInChildren<Text>().text = food.amount.ToString();
					obj.transform.Find("ItemImage").GetComponent<RawImage>().texture = food.itemImage;
				}
				else
				{
					for (int i = 0; i < item.amount; ++i)
					{
						Food food = (Food)item;
						GameObject obj = Instantiate(itemButtonPrefab, itemButtonGrid);
						int remainedTurn = food.remainedTurns[i];
						obj.GetComponent<Button>().onClick.AddListener(delegate { ChangeItem(food, remainedTurn); });
						Vector2 time = TurnManager.inst.GetTime(food.remainedTurns[i]);
						obj.GetComponentInChildren<Text>().text = time.x.ToString("00") + ":" + time.y.ToString("00");
						obj.transform.Find("ItemImage").GetComponent<RawImage>().texture = food.itemImage;
					}
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
		Vector2 time = TurnManager.inst.GetTime(remainedTurn);
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

	#region Utility Functions
	public static void IntegerTextFormatting(Text textUI, int value, bool isIgnoreZero = false)
	{
		if (value >= 0)
			textUI.color = Color.black;
		else if (value < 0)
			textUI.color = Color.red;
		if (value != 0 || !isIgnoreZero)
			textUI.text = value.ToString("+#;-#;0");
		else
			textUI.text = "";
	}
	#endregion

	#region Character Info UI Functions
	public void OpenCharacterUI()
	{
		if (openedPanel != null)
			openedPanel.SetActive(false);
		openedPanel = characterPanel;
		characterPanel.SetActive(true);
		StatusPointUI.text = gm.StatusPoint.ToString();
		MaxHealthUI.text = gm.Health.ToString() + " / " + gm.MaxHealth.ToString();
		MaxSanityUI.text = gm.Sanity.ToString() + " / " + gm.MaxSanity.ToString();
		MaxHungerUI.text = gm.Hunger.ToString() + " / " + gm.MaxHunger.ToString();
		MaxThirstUI.text = gm.Thirst.ToString() + " / " + gm.MaxThirst.ToString();
		MaxEnergyUI.text = gm.Energy.ToString() + " / " + gm.MaxEnergy.ToString();
	}

	public void CloseCharacterUI()
	{
		characterPanel.SetActive(false);
	}

	public void OnIncreaseButtonClicked(string name)
	{
		switch (name)
		{
			case "Health":
				gm.IncreaseMaxHealth();
				break;
			case "Sanity":
				gm.IncreaseMaxSanity();
				break;
			case "Hunger":
				gm.IncreaseMaxHunger();
				break;
			case "Thirst":
				gm.IncreaseMaxThirst();
				break;
			case "Energy":
				gm.IncreaseMaxEnergy();
				break;
		}
		OpenCharacterUI();
	}
	#endregion

	#region System Panel Functions
	public void ExitGame()
	{
		Application.Quit();
	}
	#endregion

	#region Option Panel Fuctions
	public void OpenOptionPanel()
	{
		optionPanel.SetActive(true);
		turnPassSlider.value = GameManager.inst.turnPassTime / 10;
		turnPassText.text = (turnPassSlider.value * 10).ToString("#.#");
	}

	public void CloseOptionPanel()
	{
		optionPanel.SetActive(false);
	}

	public void SetTurnPassTime()
	{
		GameManager.inst.turnPassTime = turnPassSlider.value * 10;
		turnPassText.text = (turnPassSlider.value * 10).ToString("#.#");
	}
	#endregion

	#region GameOver Panel Functions
	public void OpenGameOverPanel()
	{
		GameOverPanel.SetActive(true);
		if (GameManager.inst.Health <= 0)
			GameOverCharacterImage.sprite = dead_health;
		if (GameManager.inst.Sanity <= 0)
			GameOverCharacterImage.sprite = dead_sanity;
		GameOverCharacterImage.color = new Color(1, 1, 1, 0);
		GameResultText.color = new Color(1, 1, 1, 0);
		GameOverPanel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
		GameOverButton.gameObject.SetActive(false);
		GameOverText.color = new Color(1, 0, 0, 0);
		Vector2 time = TurnManager.inst.GetTime();
		GameResultText.text = TurnManager.inst.Day.ToString() + "일 " + time.x + "시간 " + time.y + "분 동안 살아남았습니다.";
		StartCoroutine(GameOverUIRoutine());
	}

	private IEnumerator GameOverUIRoutine()
	{
		yield return new WaitForSeconds(5);
		SoundManager.inst.ChangeBGM(null);
		SoundManager.inst.PlaySFX(gameOverSFX);
		Image panelImage = GameOverPanel.GetComponent<Image>();
		for (float a = 0; a <= 1; a += Time.deltaTime * 0.3f)
		{
			panelImage.color = new Color(0, 0, 0, a);
			GameOverCharacterImage.color = new Color(1, 1, 1, a);
			GameOverText.color = new Color(1, 0, 0, a);
			yield return null;
		}
		yield return new WaitForSeconds(1);
		for (float a = 0; a <= 1; a += Time.deltaTime * 0.3f)
		{
			GameResultText.color = new Color(1, 1, 1, a);
			yield return null;
		}
		GameOverButton.gameObject.SetActive(true);
	}
	#endregion
}
