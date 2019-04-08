using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ImportingAssetTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void ImportingAssetTestSimplePasses(){
	        string[] names = AssetDatabase.GetAllAssetPaths();
			AssetDatabase.Refresh();
	        Debug.Log("Somethign");
	        foreach (string name in names){
		        if (name.Contains(".png")){
			        AssetDatabase.ImportAsset(name);
			        Debug.Log(name);
		        }

	        }
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator ImportingAssetTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
