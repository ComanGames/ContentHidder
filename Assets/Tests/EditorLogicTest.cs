using Assets.AssemblingTool.Scripts;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class EditorLogicTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void UnHideDebugFolders()
        {
	        AssemblingLogic.UnHideAllInDebugFolders();
	        Assert.Pass();
        }
        [Test]
        public void CreateDebugFolder()
        {
	        AssemblingLogic.HideAllInDebugFolders();
	        Assert.Pass();
        }

        [Test]
        public void GetDebugFoldersContent()
        {
			AssemblingLogic.GetDebugFoldersContent();
			Assert.Pass();
        }

        [Test]
        public void RemoveDebugContent(){
	        
			AssemblingLogic.RemoveDebugFoldersDuplicates();
			Assert.Pass();
        }

        [Test]
        public void ZEditorImportTest()
        {
	        var allFoldersByName = AssemblingLogic.GetAllFoldersByName("Debug");
	        for (int i = 0; i < allFoldersByName.Length; i++){
				Debug.Log(allFoldersByName[i]);
	        }
			Assert.Pass();
        }

    }
}
