﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
			int interval = value - _turn;
			WeatherTurn -= interval;
			_turn = value;
			if (_turn >= 96)
			{
				Day++;
				_turn -= 96;
				DayOver();
			}
			OnTurnPassed?.Invoke(interval);
		}
	}

	public DayNight DayNight
	{
		get
		{
			Vector2 time = Time();
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

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		Weather = Weather.Cloud;
		ChangeWeather();
	}

	#region Utility Functions
	/// <summary>
	/// Calculate turn into time.
	/// 0:00 when turn = 0, 23:45 when turn = 95
	/// </summary>
	/// <returns>x is hour, y is minute</returns>
	public Vector2 Time()
	{
		Vector2 time = new Vector2
		{
			x = (Turn * 15) / 60,
			y = (Turn * 15) % 60
		};
		return time;
	}
	public Vector2 Time(int turn)
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
	}

	private void DayOver()
	{
        RotFood();
        OccurTheifEvent();
	}
    
    private void RotFood()
    {
        GameManager.inst.UseResource(food:GameManager.inst.Food * FindObjectOfType<Refrigerator>().LostFoodRate/100);
    }

    private void OccurTheifEvent()
    {
        int resourceScore = Mathf.Max(1,GameManager.inst.Food + GameManager.inst.Water + GameManager.inst.Components + 2 * GameManager.inst.Wood + 3 * (GameManager.inst.Preserved + GameManager.inst.Parts));
        int lostProbability = (int)Mathf.Clamp(8 * Mathf.Log(resourceScore,2) - FindObjectOfType<Door>().ThiefPreventionRate,0.0f, 100.0f);

        if(lostProbability> UnityEngine.Random.Range(0, 100))
        {
            //TODO : add resource loss
            Debug.Log("Event occur");
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
		UIManager.inst.UpdateWeatherUI();
	}
}
