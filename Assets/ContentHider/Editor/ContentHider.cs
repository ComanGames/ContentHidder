using UnityEditor;
using UnityEngine;

namespace Assets.ContentHider.Editor{
	public class ContentHider :EditorWindow{
		private AssemblingLogic _debugLogic; 
		private AssemblingLogic _releaseLogic; 
		[MenuItem("Window/Content Hider")]
		public static void ShowWindow(){
			GetWindow<ContentHider>().Show();
		}

		public void OnGUI(){
			titleContent = new GUIContent("Content Hider");

			LogicCreation();

			HiderForFolder(_debugLogic);
			HiderForFolder(_releaseLogic);
		}

		private static void HiderForFolder(AssemblingLogic logic){
			EditorGUILayout.LabelField(logic.Name+ " content");
			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("Hide")){
				logic.RemoveDebugContent();
				logic.HideAllInFolders();
				AssetDatabase.Refresh();
			}

			if (GUILayout.Button("Show")){
				logic.UnpackDebugFolderContent();
				AssetDatabase.Refresh();
			}

			if (GUILayout.Button("Edit")){
				logic.UnHideAllInFolders();
				AssetDatabase.Refresh();
			}

			EditorGUILayout.EndHorizontal();
		}

		private void LogicCreation(){
			if (_debugLogic == null)
				_debugLogic = new AssemblingLogic("DebugContent");
			if (_releaseLogic == null)
				_releaseLogic = new AssemblingLogic("ReleaseContent");
		}
	}
}
