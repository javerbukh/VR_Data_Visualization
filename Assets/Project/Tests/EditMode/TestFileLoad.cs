using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestFileLoad
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestFileLoadSimplePasses()
    {
        string[] input_files = new[] { "Assets/Project/Resources/test.txt" };
        LoadDataScript.LoadDataFromFiles(input_files);

        //int x = 1;
        //Assert.AreEqual(x, 1);

    }

}
