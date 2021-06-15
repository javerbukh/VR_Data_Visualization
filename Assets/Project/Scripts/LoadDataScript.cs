using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LoadDataScript : MonoBehaviour
{
    public GameObject GraphPrefab;
    public GameObject DataPrefab;
    public GameObject DataObjectPrefab;

    public GameObject newGraph;
    private int numFilesLoaded = 0;

    static float OBJECT_SCALE = 0.6f;

    public void CreateGraph()
    {
        newGraph = Instantiate(GraphPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));

    }

    public void LoadDataFromFiles(string[] input_files)
    {
        //nekwGraph = new GraphPrefab;
        //Instantiate(graph);

        foreach (string file in input_files)
        {
            Color random_color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            LoadFileIntoGraph(file);
            numFilesLoaded++;
        }
    }

    public void LoadFileIntoGraph(string input_file)
    {
        List<string> column_names = new List<string>();

        try
        {

            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader(input_file.ToString()))
            {
                int count = 0;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] split_line = line.Split(' ');
                    if (line[0] == '#')
                    {
                        foreach (string element in split_line)
                        {
                            if (element.Length > 0 && element[0].ToString() == "#")
                            {
                                column_names.Add(element.Substring(1));
                            }
                            else if (!(element == " ") && !(element == ""))
                            {
                                column_names.Add(element);
                            }
                        }
                        //for (int col = 0; col < column_names.Count; col++)
                        //{
                        //    // Add column names to data
                        //    data.GetComponent<DataScript>().AddDataColumn(column_names[col]);
                        //}
                    }
                    else
                    {

                        //for (int column = 0; column < column_names.Count; column++)
                        //{
                        // Add each element to its corresponding column
                        //data.GetComponent<DataScript>().AddDataRow(column_names[column], float.Parse(split_line[column]));
                        Vector3 dataPosition = new Vector3(float.Parse(split_line[0]), float.Parse(split_line[1]), float.Parse(split_line[2]));
                        GameObject dataPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        dataPoint.transform.position = dataPosition;
                        dataPoint.transform.localScale = new Vector3(OBJECT_SCALE, OBJECT_SCALE, OBJECT_SCALE);

                        Color random_color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                        dataPoint.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", random_color);
                        dataPoint.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");

                        GameObject data = Instantiate(DataObjectPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));



                        //}

                        // Add to count for every line after the "#" line
                        count++;
                    }
                }
            }
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Debug.Log("The file could not be read:");
            Debug.Log(e.Message);

        }
    }

    public GameObject GetGraphObject()
    {
        return newGraph;
    }

    public int GetNumFilesLoaded()
    {
        return numFilesLoaded;
    }
}
