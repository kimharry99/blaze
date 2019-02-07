using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

public class EventManager : SingletonBehaviour<EventManager>
{
	private Dictionary<string, EventInfo> events = new Dictionary<string, EventInfo>();

	private void Awake()
	{
		//UnityAction action = delegate { UIManager.inst.CloseEventLogPanel(); };
		//BinaryFormatter binaryFormatter = new BinaryFormatter();

		//UnityActionContainer container = new UnityActionContainer(action);
		//Stream stream = new FileStream(Application.dataPath + "/Resources/Binaries/test.bin", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
		//binaryFormatter.Serialize(stream, container);

		////Stream stream = new FileStream(Application.dataPath + "/Resources/Binaries/test.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
		////stream = new MemoryStream(Resources.Load<TextAsset>("Binaries/test").bytes);
		//container = (UnityActionContainer)binaryFormatter.Deserialize(stream);
		//container.action();

		foreach (var json in Resources.LoadAll<TextAsset>("Jsons/EventInfo"))
		{
			EventInfo info = JsonUtility.FromJson<EventInfo>(json.text);
			events.Add(info.eventName, info);
		}

		
	}

	public EventInfo GetEventInfo(string eventName)
	{
		return events[eventName];
	}

	public void SaveEventData()
	{
		JsonHelper.JsonToFile(JsonUtility.ToJson(events.Values.ToList()),"Save/EventData.json");
	}

	public void LoadEventData()
	{
		List<EventInfo> infos =	JsonUtility.FromJson<List<EventInfo>>(JsonHelper.LoadJson("Save/EventData"));
		foreach (var info in infos)
		{
			events.Add(info.eventName, info);
		}
	}
}