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
        int numData = GraphScript.GetNumData();
        Assert.AreEqual(0, numData);

        Vector3 newPos = PlayerScript.MoveTo(1, "Test");
        float location = (1.0f / 10) * 20.0f;
        Assert.AreEqual(new Vector3(location, location, location), newPos);

        Vector3 newPos8 = PlayerScript.MoveTo(8, "Test");
        float location8 = (8.0f / 10) * 20.0f;
        Assert.AreEqual(new Vector3(location8, location8, location8), newPos8);
    }

}
