﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Weather
{
	Rain = -1,
	Cloud,
	Sun
}

public enum DayNight
{
	Night = -1,
	Sunset,
	Day
}
public class TurnManager : SingletonBehaviour<TurnManager>
{
	public event Action<int> OnTurnPassed;

	#region Turn System Variables
	public int Day { get; private set; }
	private int _turn;
	public int Turn
	{
		get
		{
			return _turn;
		}
		private set
		{
			if (GameManager.inst.IsGameOver)
				return;
			int interval = value - _turn;
			WeatherTurn -= interval;
			_turn = value;
			if (_turn >= 96)
			{
				Day++;
				_turn -= 96;
				DayOver();
			}
			if(_turn == 24)
			{
				SoundManager.inst.ChangeBGM(dayBGM);
			}
			if (_turn == 72)
			{
				SoundManager.inst.ChangeBGM(nightBGM);
			}
			OnTurnPassed?.Invoke(interval);
		}
	}

	public DayNight DayNight
	{
		get
		{
			Vector2 time = GetTime();
			if (time.x <= 5 || time.x >= 18)
			{
				return DayNight.Night;
			}
			else if (time.x <= 7 || time.x >= 16)
			{
				return DayNight.Sunset;
			}
			else
			{
				return DayNight.Day;
			}
		}
	}

	[SerializeField]
	private AudioClip dayBGM, nightBGM;
	#endregion

	#region Weather System Variables
	public Weather Weather { get; private set; }
	private int _weatherTurn;
	public int WeatherTurn {
		get { return _weatherTurn; }
		set
		{
			if (value <= 0)
			{
				ChangeWeather();
			}
			else
			{
				_weatherTurn = value;
			}

			if (FindObjectOfType<DigitalRuby.RainMaker.RainScript2D>() != null && Weather == Weather.Rain)
			{
				FindObjectOfType<DigitalRuby.RainMaker.RainScript2D>().RainIntensity = Mathf.Clamp(_weatherTurn / 20f, 0, 1);
			}
		}
	}
	#endregion

	protected override void Awake()
	{
		if (inst != this)
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
		Weather = Weather.Cloud;
		ChangeWeather();
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name == "Title")
			return;
		if(DayNight == DayNight.Night)
			SoundManager.inst.ChangeBGM(nightBGM);
		else
			SoundManager.inst.ChangeBGM(dayBGM);

		OnTurnPassed(0);
	}

	#region Utility Functions
	/// <summary>
	/// Calculate turn into time.
	/// 0:00 when turn = 0, 23:45 when turn = 95
	/// </summary>
	/// <returns>x is hour, y is minute</returns>
	public Vector2 GetTime()
	{
		Vector2 time = new Vector2
		{
			x = (Turn * 15) / 60,
			y = (Turn * 15) % 60
		};
		return time;
	}
	public Vector2 GetTime(int turn)
	{
		Vector2 time = new Vector2
		{
			x = (turn * 15) / 60,
			y = (turn * 15) % 60
		};
		return time;
	}
	#endregion

	public void UseTurn(int turn)
	{
		
		for (int i = 0; i < turn; ++i)
		{
			Turn++;
		}
		
		//StartCoroutine(TurnPassRoutine(turn, GameManager.inst.turnPassTime / turn));
	}

	private IEnumerator TurnPassRoutine(int remainedTurn, float timer)
	{
		RectTransform minuteHand = UIManager.inst.minuteHand;
		RectTransform hourHand = UIManager.inst.hourHand;

		for (float i = timer; i > 0; i -= Time.deltaTime)
		{
			minuteHand.Rotate(0, 0, 30 * Time.deltaTime / timer);
			hourHand.Rotate(0, 0, 7.5f * Time.deltaTime / timer);
			yield return null;
		}

		if (remainedTurn > 1)
			StartCoroutine(TurnPassRoutine(remainedTurn - 1, timer));
	}

	private void DayOver()
	{
        RotFood();
        OccurTheifEvent();
		GameManager.inst.GetExperiencePoint(4);
	}
    
    private void RotFood()
    {
		if (GameManager.inst.Food > 0)
		{
			EventManager.inst.StartEvent("RotFood");
		}
    }

    private void OccurTheifEvent()
    {
		int resourceScore = Mathf.Max(1,GameManager.inst.Food + GameManager.inst.Water + GameManager.inst.Components + 2 * GameManager.inst.Wood + 3 * (GameManager.inst.Preserved + GameManager.inst.Parts));
		float lostProbability = Mathf.Clamp(8 * Mathf.Log(resourceScore, 2) - ((Door)GameManager.inst.furnitures["Door"]).ThiefPreventionRate, 0.0f, 100.0f);

		Debug.Log(lostProbability);

        if(lostProbability> UnityEngine.Random.Range(0f, 100f))
        {
			EventManager.inst.StartEvent("Theif");
        }
    }

	public void ChangeWeather()
	{
		int rand = UnityEngine.Random.Range(0, 100);
		switch (Weather)
		{
			case Weather.Sun:
				if (rand < 30)
				{
					Weather = Weather.Cloud;
				}
				break;
			case Weather.Cloud:
				if (rand < 30)
				{
					Weather = Weather.Sun;
				}
				else if (rand > 69)
				{
					Weather = Weather.Rain;
				}
				break;
			case Weather.Rain:
				if (rand < 80)
				{
					Weather = Weather.Cloud;
				}
				break;
		}
		_weatherTurn = UnityEngine.Random.Range(20, 40);
		if (Weather == Weather.Rain && FindObjectOfType<DigitalRuby.RainMaker.RainScript2D>() != null)
		{
			FindObjectOfType<DigitalRuby.RainMaker.RainScript2D>().RainIntensity = Mathf.Clamp(_weatherTurn / 20f, 0, 1);
		}
		else if (FindObjectOfType<DigitalRuby.RainMaker.RainScript2D>() != null)
		{
			FindObjectOfType<DigitalRuby.RainMaker.RainScript2D>().RainIntensity = 0;
		}
		UIManager.inst.UpdateWeatherUI();
	}
}
