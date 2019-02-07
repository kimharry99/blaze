using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class LogEvent : ScriptableObject
{
	private const string path = "Assets/Resources/LogEvents/";

	public string eventName;
	public string eventTitle;
	public string description;
	public List<string> actionDescriptions;

	public abstract void EventStart();
	public abstract List<UnityAction> GetActions();
}
