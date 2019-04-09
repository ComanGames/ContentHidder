using System.Collections;
using System.IO;
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

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator EditorLogicTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
