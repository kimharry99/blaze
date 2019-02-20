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

	#region Prefabs
	[Header("Prefabs")]
	public GameObject eventButtonPrefab;
	public GameObject resultPrefab;
	public GameObject buffUIPrefab;
	public GameObject diseaseButtonPrefab;
	public GameObject resourceInfoPrefab;
	#endregion

	#region Managers
	private TurnManager tm;
	private GameManager gm;
	#endregion

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
			InitResultObject(result, "Textures/Resources Icon/Food", food);
		}
		if (preserved != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Textures/Resources Icon/Preserved", preserved);
		}
		if (water != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Textures/Resources Icon/Water", water);
		}
		if (wood != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Textures/Resources Icon/Wood", wood);
		}
		if (components != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Textures/Resources Icon/Components", components);
		}
		if (parts != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Textures/Resources Icon/Parts", parts);
		}
	}

	public void AddPlayerStatusResult(int health = 0, int sanity = 0, int hunger = 0, int thirst = 0, int energy = 0)
	{
		GameObject result;
		if (health != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Textures/PlayerStatus Icon/Health", health);
		}
		if (sanity != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Textures/PlayerStatus Icon/Sanity", sanity);
		}
		if (hunger != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Textures/PlayerStatus Icon/Hunger", hunger);
		}
		if (thirst != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Textures/PlayerStatus Icon/Thirst", thirst);
		}
		if (energy != 0)
		{
			result = Instantiate(resultPrefab, resultGrid);
			InitResultObject(result, "Textures/PlayerStatus Icon/Energy", energy);
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
			if (!buffUIs.ContainsKey(buff.buffName))
			{
				buffUI = Instantiate(buffUIPrefab, buffPanel.transform).GetComponent<BuffUI>();
				buffUI.transform.Find("IconImage").GetComponent<RawImage>().texture = buff.buffTexture;
				buffUI.Init(buff.buffName, buff.description);
				buffUIs.Add(buff.buffName, buffUI);
			}
			else
			{
				buffUI = buffUIs[buff.buffName];
				buffUI.gameObject.SetActive(true);
			}
			if (buff.BuffCount > 0)
				buffUI.transform.Find("BuffCountText").GetComponent<Text>().text = "x" + buff.BuffCount.ToString();
			if (buff.RemainedTurn > 0)
			{
				Vector2 time = tm.Time(buff.RemainedTurn);
				buffUI.transform.Find("BuffTimeText").GetComponent<Text>().text = time.x.ToString("00") + ":" + time.y.ToString("00");
			}
		}
		else if (buffUIs.ContainsKey(buff.buffName))
		{
			buffUIs[buff.buffName].gameObject.SetActive(false);
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
		obj.GetComponent<Button>().onClick.AddListener(delegate { ChangeDisease(disease); });
	}

	public void ChangeDisease(Disease disease)
	{
		cureButton.onClick.RemoveAllListeners();
		cureButton.onClick.AddListener(delegate {
			disease.Cure();
			OpenCurePanel();
			UpdateBuffUI(disease);
			cureButton.onClick.RemoveAllListeners();
		});
		cureButton.interactable = disease.IsCureable;
		foreach (var str in disease.cureInfoString.Split(' ', '\n'))
		{
			int curResource = 0;
			GameObject obj = Instantiate(resourceInfoPrefab, cureInfoGrid);
			string path = "Textures/";
			string[] info = str.Split(',');
			//TODO
			switch (info[0])
			{
				case "Water":
					curResource = gm.Water;
					goto case "Dummy";
				case "Food":
					curResource = gm.Food;
					goto case "Dummy";
				case "Components":
					curResource = gm.Components;
					goto case "Dummy";
				case "Parts":
					curResource = gm.Parts;
					goto case "Dummy";
				case "Preserved":
					curResource = gm.Preserved;
					goto case "Dummy";
				case "Wood":
					curResource = gm.Wood;
					goto case "Dummy";
				case "Dummy":
					path += ("Resources Icon/" + info[0]);
					break;
				default:
					path += ("Items Icon/" + info[0]);
					break;
			}
			obj.transform.Find("RawImage").GetComponent<RawImage>().texture = Resources.Load<Texture2D>(path);
			obj.GetComponentInChildren<Text>().text = curResource + "/" + info[1];

		}	
	}
	#endregion
}
