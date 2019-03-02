using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : SingletonBehaviour<EventManager>
{
	private Dictionary<string, LogEvent> events = new Dictionary<string, LogEvent>();

	private void Awake()
	{
		SetStatic();
		foreach (var logEvent in Resources.LoadAll<LogEvent>("LogEvents/"))
		{
			events.Add(logEvent.eventName, logEvent);
		}
	}

	public LogEvent GetEvent(string eventName)
	{
		return events[eventName];
	}

	public void StartEvent(string eventName)
	{
		UIManager.inst.OpenEventLogPanel(events[eventName]);
	}
}

[Serializable]
public class LogEventInfo
{
	public string eventName;
	public string title;
	public string description;
	public List<string> actionDescriptions;
	public UnityAction action;

	public LogEventInfo(string eventName, string title, string description, List<string> actionDescriptions)
	{
		this.eventName = eventName;
		this.title = title;
		this.description = description;
		this.actionDescriptions = actionDescriptions;
	}
}