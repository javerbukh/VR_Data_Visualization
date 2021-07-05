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
    private static float MOVE_SPEED; 

    // Start is called before the first frame update
    void Start()
    {
        print("Welcome! Press q to start");
        loadData = Instantiate(LoadDataPrefab);
        MOVE_SPEED = Time.deltaTime * 5;
    }

    // Update is called once per frame
    void Update()
    {
        // print("Here");
        if (Input.GetKeyDown("q"))
        {
            print("Loading data...");
            // string[] input_files = new[] { "Assets/Project/Resources/test.txt", "Assets/Project/Resources/gaia_data.txt" };
            //string[] input_files = new[] { "Assets/Project/Resources/gaia_data.txt" };
            //string[] input_files = new[] { "Assets/Project/Resources/gaia_200lyr.txt", "Assets/Project/Resources/gaia_data.txt" };
            string[] input_files = new[] { "Assets/Project/Resources/gaia_200lyr.txt" };


            //currentFileReader.GetComponent<FileReaderScript>().LoadDataFromFiles(currentGraph, input_files);

            // LoadDataPrefab.GetComponent<LoadDataScript>().LoadDataFromFiles(input_files);
            LoadData(input_files);
            //LoadDataScript.LoadDataFromFiles(input_files);



        }
        if (Input.GetKey("w"))
        {
            this.transform.position += this.transform.forward * MOVE_SPEED;
        }
        if (Input.GetKey("a"))
        {
            this.transform.position -= this.transform.right * MOVE_SPEED;
        }
        if (Input.GetKey("s"))
        {
            this.transform.position -= this.transform.forward * MOVE_SPEED;

        }
        if (Input.GetKey("d"))
        {
            this.transform.position += this.transform.right * MOVE_SPEED;

        }
        if (Input.GetKey("z"))
        {
            this.transform.position += this.transform.up * MOVE_SPEED;

        }
        if (Input.GetKey("c"))
        {
            this.transform.position -= this.transform.up * MOVE_SPEED;

        }

        if (Input.GetKey("r"))
        {
            this.transform.position = new Vector3(0, 0, 0);

        }

        if (Input.GetKey("p"))
        {
            print(this.transform.position);

        }

        if (Input.GetKey("up"))
        {
            MOVE_SPEED = MOVE_SPEED * 2;

        }

        if (Input.GetKey("down"))
        {
            MOVE_SPEED = MOVE_SPEED / 2;

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
