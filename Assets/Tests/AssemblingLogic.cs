using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.AssemblingTool.Scripts{
	public static class AssemblingLogic 
	{

		public static void HideAllInDebugFolders(){
			HideFoldersByName("Debug");
			AssetDatabase.Refresh();
		}
		public static void UnHideAllDebugFolders(){
			UnHideFoldersByName(".Debug");
			AssetDatabase.Refresh();
		}

		private static void UnHideFoldersByName( string name){
			string[] debugFolders = GetAllFoldersByName(name);

			for (int i = 0; i < debugFolders.Length; i++){
				string oldName = debugFolders[i];
				string newName = oldName.Substring(0,oldName.Length-name.Length-1);
				MoveDirectory(oldName,newName);
			}
		}
		private static void HideFoldersByName( string name){
			string[] debugFolders = GetAllFoldersByName(name);

			for (int i = 0; i < debugFolders.Length; i++){
				string oldName = debugFolders[i];
				string newName = oldName.Replace(name, "." + name);
				MoveDirectory(oldName,newName);
			}
		}


		public static string[] GetAllFoldersByName(string name){
			List<string> paths =  new List<string>();
			string rootFolder = Application.dataPath;
			string[] subFolders = GetSub(rootFolder);

			for (int i = 0; i < subFolders.Length; i++){
				string s = subFolders[i];
				if(s.EndsWith(name)&&!s.EndsWith("."+name))
					paths.Add(s);
			}

			return paths.ToArray();
		}

		private static string[] GetSub(string root){
			List<string> output = new List<string>();
			output.Add(root);

			string[] subs = Directory.GetDirectories(root);
			for (int i = 0; i < subs.Length; i++){

				string[] s = GetSub(subs[i]);

				if(s.Length>0)
					output.AddRange(s);
			}

			return output.ToArray();
		}

		public static void MoveDirectory(string source, string target)
		{
			var stack = new Stack<Folders>();
			stack.Push(new Folders(source, target));

			while (stack.Count > 0)
			{
				var folders = stack.Pop();
				Directory.CreateDirectory(folders.Target);
				foreach (var file in Directory.GetFiles(folders.Source, "*.*"))
				{
					string targetFile = Path.Combine(folders.Target, Path.GetFileName(file));
					if (File.Exists(targetFile)) File.Delete(targetFile);
						File.Move(file, targetFile);
				}

				foreach (var folder in Directory.GetDirectories(folders.Source))
				{
					stack.Push(new Folders(folder, Path.Combine(folders.Target, Path.GetFileName(folder))));
				}
			}
			Directory.Delete(source, true);
		}

		public class Folders
		{
			public string Source { get; private set; }
			public string Target { get; private set; }

			public Folders(string source, string target)
			{
				Source = source;
				Target = target;
			}
		}
	}
}
