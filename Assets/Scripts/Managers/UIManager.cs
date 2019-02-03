using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
}
