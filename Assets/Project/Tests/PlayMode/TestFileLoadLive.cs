using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestFileLoadLive
{

    public GameObject PlayerPrefab;

    [UnityTest]
    public IEnumerator TestFileLoadLiveWithEnumeratorPasses()
    {
        //var gameObject = new GameObject();
        //var player = gameObject.AddComponent<PlayerScript>();
        string[] input_files = new[] { "Assets/Project/Resources/test.txt" };

        //GameObject player = Instantiate(PlayerPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));


        //PlayerScript.LoadData(input_files);
        //LoadDataScript.LoadDataFromFiles(input_files);
        //player.GetComponent<PlayerScript>().LoadData(input_files);
        //PlayerScript player2 = new PlayerScript();
        //player2.LoadData(input_files);

        //GameObject player = GameObject.Find("Player");

        //player.GetComponent<PlayerScript>().LoadData(input_files);
        //PlayerScript.PublicStart();
        //int numFilesLoadedBefore = PlayerScript.GetNumFilesLoaded();
        int numFilesLoaded = PlayerScript.GetNumber();

        //PlayerScript.LoadData(input_files);

        yield return new WaitForSeconds(2);



        //LoadDataScript.getGraphObject().GetComponent<GraphScript>().GetNumData();

        //int graphNumData = graph.GetComponent<GraphScript>().GetNumData();
        //var loader = gameObject.AddComponent<LoadDataScript>();
        //int numFilesLoaded = PlayerScript.GetNumFilesLoaded();

        //Assert.AreEqual(numFilesLoadedBefore, 0);
        Assert.AreEqual(numFilesLoaded, 1);

    }
}
