using UnityEditor;
using UnityEngine;

namespace Assets.ContentHider.Editor{
	public class ContentHider :EditorWindow 
	{
		[MenuItem("Window/Content Hider")]
		public static void ShowWindow(){
			GetWindow<ContentHider>().Show();
		}

		public void OnGUI(){
			titleContent = new GUIContent("Content Hider");

			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("Show"))
			{
			}
			if (GUILayout.Button("Hide"))
			{
			}
			if (GUILayout.Button("Edit"))
			{
			}
			EditorGUILayout.EndHorizontal();

		}
	}
}
