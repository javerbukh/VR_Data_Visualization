using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject LoadDataPrefab;

    // Start is called before the first frame update
    void Start()
    {
        print("Welcome! Press a to start");

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
            LoadDataPrefab.GetComponent<LoadDataScript>().LoadDataFromFiles(input_files);


        }
    }

    public static int GetNumber()
    {
        return 1;
    }
}
