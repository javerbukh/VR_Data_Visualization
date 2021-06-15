using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject LoadDataPrefab;

    private static GameObject loadData;

    // Start is called before the first frame update
    void Start()
    {
        print("Welcome! Press a to start");
        loadData = Instantiate(LoadDataPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        // print("Here");
        if (Input.GetKeyDown("a"))
        {
            print("Loading data...");
            string[] input_files = new[] { "Assets/Project/Resources/test.txt" };

            //currentFileReader.GetComponent<FileReaderScript>().LoadDataFromFiles(currentGraph, input_files);

            // LoadDataPrefab.GetComponent<LoadDataScript>().LoadDataFromFiles(input_files);
            LoadData(input_files);
            //LoadDataScript.LoadDataFromFiles(input_files);



        }
    }

    public static int GetNumber()
    {
        return 1;
    }

    public void LoadData(string[] input_files)
    {
        loadData.GetComponent<LoadDataScript>().LoadDataFromFiles(input_files);
    }

    public static int GetNumFilesLoaded()
    {
        return loadData.GetComponent<LoadDataScript>().GetNumFilesLoaded();
    }
}
