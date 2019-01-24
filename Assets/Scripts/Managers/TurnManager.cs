using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weather
{
	Sun,
	Cloud,
	Rain
}

public class TurnManager : SingletonBehaviour<TurnManager>
{
	public delegate void IntEvent(int turn);
	public event IntEvent OnTurnPassed;

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
		}
	}
	#endregion

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		Weather = Weather.Sun;
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
		Turn += turn;
		OnTurnPassed(turn);
	}

	private void DayOver()
	{
		//TODO : Handle dayover event
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
				if (rand < 30)
				{
					Weather = Weather.Cloud;
				}
				break;
		}
		_weatherTurn = UnityEngine.Random.Range(20, 40);
		HomeUIManager.inst.UpdateWeatherUI();
	}
}
