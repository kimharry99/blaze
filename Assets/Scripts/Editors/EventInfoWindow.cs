using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
public class EventInfoWindow : EditorWindow
{
	string eventName;
	string title;
	string description;
	List<string> actionDescriptions = new List<string>();
	int size;

	// Add menu named "My Window" to the Window menu
	[MenuItem("Editors/이벤트 정보")]
	static void Init()
	{
		// Get existing open window or if none, make a new one:
		EventInfoWindow window = (EventInfoWindow)GetWindow(typeof(EventInfoWindow));
		window.name = "이벤트 정보 관리";
		window.Show();
	}

	void OnGUI()
	{
		GUILayout.Label("인덱싱 정보", EditorStyles.helpBox);
		eventName = EditorGUILayout.TextField("이벤트 이름", eventName);

		GUILayout.Label("내용", EditorStyles.helpBox);
		title = EditorGUILayout.TextField("이벤트 제목", title);
		GUILayout.Label("이벤트 설명", EditorStyles.boldLabel);
		description = EditorGUILayout.TextArea(description);

		GUILayout.Label("행동 버튼 내용 리스트");
		size = EditorGUILayout.IntField("리스트 크기",size);

		if (actionDescriptions.Count < size)
		{
			for (; actionDescriptions.Count < size; actionDescriptions.Add(""));
		}
		else if (actionDescriptions.Count > size)
		{
			for (int i = size; actionDescriptions.Count > size; actionDescriptions.RemoveAt(--i)) ;
		}

		for (int i = 0; i < size; ++i)
		{
			actionDescriptions[i] = EditorGUILayout.TextField(i.ToString() + ": ", actionDescriptions[i]);
		}

		if (GUILayout.Button("불러오기"))
		{
			string json = JsonHelper.LoadJson("EventInfo/" + eventName);
			EventInfo info = JsonUtility.FromJson<EventInfo>(json);
			title = info.title;
			description = info.description;
			actionDescriptions = info.actionDescriptions;
			AssetDatabase.Refresh();
		}

		if (GUILayout.Button("편집"))
		{
			EventInfo info = new EventInfo(eventName, title, description, actionDescriptions);
			JsonHelper.JsonToFile(JsonUtility.ToJson(info, true), "EventInfo/" + eventName + ".json");
			AssetDatabase.Refresh();
		}
	}
}
#endif