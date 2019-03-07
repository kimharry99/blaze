using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyScript : MonoBehaviour
{
	public GameObject skyObject;
	public Light sunLight;
	public Light playerSpotlight;

	[SerializeField]
	private 
		Vector3 dayPos, nightPos1, nightPos2;

	private void Awake()
	{
		TurnManager.inst.OnTurnPassed += OnTurnPassed;
	}

	private void OnDestroy()
	{
		TurnManager.inst.OnTurnPassed -= OnTurnPassed;
	}

	private void OnTurnPassed(int turn)
	{
		switch (TurnManager.inst.Weather)
		{
			case Weather.Sun:
				sunLight.intensity = 1;
				break;
			case Weather.Cloud:
				sunLight.intensity = 0.8f;
				break;
			case Weather.Rain:
				sunLight.intensity = 0.5f;
				break;
		}

		if (TurnManager.inst.DayNight == DayNight.Day)
		{
			skyObject.transform.position = dayPos;
			playerSpotlight.intensity = 0;
		}
		else if (TurnManager.inst.DayNight == DayNight.Night)
		{
			skyObject.transform.position = nightPos1;
			playerSpotlight.intensity = 15;
		}
		else
		{
			if (TurnManager.inst.Turn < 48)
			{
				skyObject.transform.position = nightPos1 + (dayPos - nightPos1) * (TurnManager.inst.Turn - 24) / 8f;
				playerSpotlight.intensity = 15 * (1 - (TurnManager.inst.Turn - 24) / 8f);
			}
			else
			{
				skyObject.transform.position = dayPos + (nightPos2 - dayPos) * (TurnManager.inst.Turn - 64) / 8f;
				playerSpotlight.intensity = 15 * (TurnManager.inst.Turn - 64) / 8f;
			}
		}
		sunLight.transform.rotation = Quaternion.Euler(180 + 3.75f * TurnManager.inst.Turn, 0, 0);
	}

	public Vector3 SkyMovement()
	{
		if (TurnManager.inst.DayNight != DayNight.Sunset)
		{
			return Vector3.zero;
		}
		else
		{
			if (TurnManager.inst.Turn < 48)
			{
				return (dayPos - nightPos1) / 8f;
			}
			else
			{
				return (nightPos2 - dayPos) / 8f;
			}
		}
	}

	public float SpotLightItensityDelta()
	{
		if (TurnManager.inst.DayNight != DayNight.Sunset)
		{
			return 0;
		}
		else
		{
			if (TurnManager.inst.Turn < 48)
			{
				return -15 / 8f;
			}
			else
			{
				return 15 / 8f;
			}
		}
	}
}
