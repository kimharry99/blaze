﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class LogEvent : ScriptableObject
{
	private const string path = "Assets/Resources/LogEvents/";

	public string eventName;
	public string eventTitle;
	[TextArea]
	public string description;
	public List<string> actionDescriptions;
	public Texture2D eventTexture;

	public abstract void EventStart();
	public abstract List<UnityAction> GetActions();

	public void NextEvent(string eventName)
	{
		UIManager.inst.OpenEventLogPanel(EventManager.inst.GetEvent(eventName));
	}

	public void EndEvent()
	{
		UIManager.inst.CloseEventLogPanel();
	}
}
