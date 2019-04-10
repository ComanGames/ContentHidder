using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.ContentHider.Editor{
	public  class AssemblingLogic 
	{
		private string name ;
		public AssemblingLogic(string name){
			this.name = name;
		}

		public void HideAllInFolders(){
			HideFoldersByName(name);
			AssetDatabase.Refresh();
		}
		public void UnHideAllInFolders(){
			UnHideFoldersByName(name);
			AssetDatabase.Refresh();
		}

		public  void UnpackDebugFolderContent(){
			UnPackFoldersByName("."+name);
			AssetDatabase.Refresh();
		}

		public void RemoveDebugContent(){
			RemoveFoldersContentByName("."+name);
			AssetDatabase.Refresh();
		}

		private void RemoveFoldersContentByName(string name){
			string[] folders = GetAllFoldersByName(name);
			foreach (var oldName in folders){
				string newName = oldName.Substring(0,oldName.Length-name.Length-1);
				RemoveFilesIfDublicate(oldName, newName);
			}
		}

		private void UnHideFoldersByName( string name){
			string[] folders = GetAllFoldersByName("." +name);

			for (int i = 0; i < folders.Length; i++){
				string oldName = folders[i];
				string newName = oldName.Substring(0,oldName.Length-name.Length-1)+name;
				MoveDirectory(oldName,newName);
			}
		}
		private void UnPackFoldersByName( string name){
			string[] folders = GetAllFoldersByName(name);

			for (int i = 0; i < folders.Length; i++){
				string oldName = folders[i];
				string newName = oldName.Substring(0,oldName.Length-name.Length-1);
				MoveFiles(oldName,newName);
			}
		}

		private void HideFoldersByName( string name){
			string[] debugFolders = GetAllFoldersByName(name);

			for (int i = 0; i < debugFolders.Length; i++){
				string oldName = debugFolders[i];
				string newName = oldName.Replace(name, "." + name);
				MoveDirectory(oldName,newName);
			}
		}


		public string[] GetAllFoldersByName(string name){
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

		private string[] GetSub(string root){
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

		private void RemoveFilesIfDublicate(string source, string target){
			var stack = new Stack<AssemblerFolder>();
			stack.Push(new AssemblerFolder(source, target));

			while (stack.Count > 0)
			{
				var folders = stack.Pop();
				foreach (var file in Directory.GetFiles(folders.Source, "*.*"))
				{
					string targetFile = Path.Combine(folders.Target, Path.GetFileName(file));
					if (File.Exists(targetFile)){
						try{
							File.Delete(targetFile);
						}
						catch (IOException ){
							File.Delete(targetFile);
						}
					}
				}

				if(target!=folders.Target&&Directory.Exists(folders.Target))
					Directory.Delete(folders.Target);

				foreach (var folder in Directory.GetDirectories(folders.Source))
				{
						stack.Push(new AssemblerFolder(folder, Path.Combine(folders.Target, Path.GetFileName(folder))));
				}
			}
		}

		public void MoveFiles(string source, string target)
		{
			var stack = new Stack<AssemblerFolder>();
			stack.Push(new AssemblerFolder(source, target));

			while (stack.Count > 0)
			{
				var folders = stack.Pop();
				Directory.CreateDirectory(folders.Target);
				foreach (var file in Directory.GetFiles(folders.Source, "*.*"))
				{
					string targetFile = Path.Combine(folders.Target, Path.GetFileName(file));
					if (!File.Exists(targetFile)) 
						File.Copy(file, targetFile);
				}

				foreach (var folder in Directory.GetDirectories(folders.Source))
				{
					stack.Push(new AssemblerFolder(folder, Path.Combine(folders.Target, Path.GetFileName(folder))));
				}
			}
		}

		public void MoveDirectory(string source, string target)
		{
			var stack = new Stack<AssemblerFolder>();
			stack.Push(new AssemblerFolder(source, target));

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
					stack.Push(new AssemblerFolder(folder, Path.Combine(folders.Target, Path.GetFileName(folder))));
				}
			}
			Directory.Delete(source, true);
		}
	}
}
