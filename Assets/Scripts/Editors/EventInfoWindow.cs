using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
public class EventInfoWindow : EditorWindow
{
	private const string path = "Assets/Resources/LogEvents/";

	string eventName;
	string eventTitle;
	string description;
	List<string> actionDescriptions = new List<string>();
	int size;

	MonoScript script;

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
		GUILayout.Label("이벤트 스크립트", EditorStyles.helpBox);
		script = EditorGUILayout.ObjectField(script, typeof(MonoScript), false) as MonoScript;

		GUILayout.Label("인덱싱 정보", EditorStyles.helpBox);
		eventName = EditorGUILayout.TextField("이벤트 이름", eventName);


		GUILayout.Label("내용", EditorStyles.helpBox);
		eventTitle = EditorGUILayout.TextField("이벤트 제목", eventTitle);

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

		if (GUILayout.Button("편집"))
		{
			LogEvent logEvent = CreateInstance(script.GetClass()) as LogEvent;

			logEvent.eventName = eventName;
			logEvent.eventTitle = eventTitle;
			logEvent.description = description;
			logEvent.actionDescriptions = actionDescriptions;

			AssetDatabase.CreateAsset(logEvent, path + script.GetClass().Name + ".asset");
			AssetDatabase.Refresh();
		}
	}
}
#endif