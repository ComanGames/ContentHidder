using System.Collections;
using Assets.AssemblingTool.Scripts;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class EditorLogicTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void UnHideDebugFolders()
        {
			AssemblingLogic.UnHideAllDebugFolders();
			Assert.Pass();
        }

		[Test]
        public void RemoveDebugContent(){
	        
			AssemblingLogic.RemoveDebugFoldersDuplicates();
			Assert.Pass();
        }
        [Test]
        public void CreateDebugFolder()
        {
			AssemblingLogic.HideAllInDebugFolders();
			Assert.Pass();
        }
        [Test]
        public void EditorLogicTestSimplePasses()
        {
	        var allFoldersByName = AssemblingLogic.GetAllFoldersByName("Debug");
	        for (int i = 0; i < allFoldersByName.Length; i++){
				Debug.Log(allFoldersByName[i]);
	        }
			Assert.Pass();
        }

    }
}
