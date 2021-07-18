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
    public GameObject PlanetPrefab;

    private static GameObject graph;
    private int numFilesLoaded = 0;

    public Mesh mesh;
    public Material material;

    [SerializeField]
    public float OBJECT_SCALE = 0.6f;

    [SerializeField]
    public float GRAPH_SCALE = 100.0f;

    [SerializeField]
    public int NUM_AXES = 10;

    public void CreateGraph()
    {
        graph = Instantiate(GraphPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        graph.GetComponent<GraphScript>().graph_size = GRAPH_SCALE;
        graph.GetComponent<GraphScript>().CreateAxes(NUM_AXES);

    }

    public float GetGraphScale()
    {
        return GRAPH_SCALE;
    }

    public int GetGraphAxes()
    {
        return NUM_AXES;
    }

    public void LoadDataFromFiles(string[] input_files)
    {
        //nekwGraph = new GraphPrefab;
        //Instantiate(graph);
        CreateGraph();

        foreach (string file in input_files)
        {
            Color random_color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            GameObject newData = (GameObject)Instantiate(DataPrefab, graph.transform.position, Quaternion.Euler(0, 0, 0));
            newData.transform.parent = graph.transform;
            newData.GetComponent<DataScript>().SetInputFile(file);
            newData.GetComponent<DataScript>().color = random_color;
            graph.GetComponent<GraphScript>().AddData(newData);

            ReadFile(newData);
            newData.GetComponent<DataScript>().GetMaxesAndMins();
            newData.GetComponent<DataScript>().FindXYZIndices();


            //LoadFileIntoGraph(file);
            numFilesLoaded++;
        }

        List<GameObject> dataCollection = graph.GetComponent<GraphScript>().dataCollection;


        Dictionary<string, float> graph_maxes = graph.GetComponent<GraphScript>().maxes;
        Dictionary<string, float> graph_mins = graph.GetComponent<GraphScript>().mins;

        foreach (GameObject data_m in dataCollection)
        {
            float[] mins = data_m.GetComponent<DataScript>().mins;
            float[] maxes = data_m.GetComponent<DataScript>().maxes;

            int x_col_index = data_m.GetComponent<DataScript>().x_col_index;
            int y_col_index = data_m.GetComponent<DataScript>().y_col_index;
            int z_col_index = data_m.GetComponent<DataScript>().z_col_index;


            int[] positions = new int[3] { x_col_index, y_col_index, z_col_index };
            string[] xyz = new string[3] { "x", "y", "z" };
            for (int index = 0; index < 3; index++)
            {
                string letter = xyz[index];
                int pos = positions[index];
                if (graph_mins[letter] > mins[pos])
                {
                    graph_mins[letter] = mins[pos];
                }
                if (graph_maxes[letter] < maxes[pos])
                {
                    graph_maxes[letter] = maxes[pos];
                }
            }
        }

        graph.GetComponent<GraphScript>().maxes = graph_maxes;
        graph.GetComponent<GraphScript>().mins = graph_mins;

        print("x_max: " + graph_maxes["x"] + "y_max: " + graph_maxes["y"] + "z_max: " + graph_maxes["z"]);
        print("x_min: " + graph_mins["x"] + "y_min: " + graph_mins["y"] + "z_min: " + graph_mins["z"]);

        // Once all maxes and mins are found for the graph, visualize
        // the data
        foreach (GameObject data in dataCollection)
        {

            Color sphere_color = data.GetComponent<DataScript>().color;
            print(sphere_color);
            // VisualizeDataAsParticles(ps, graph, data, sphere_color);
            VisualizeData(graph, data, sphere_color);
        }
    }

    public void LoadDataFromFile(string input_file)
    {
        /*
         * Loads Data from a single file and creates a Data object out of it
         * 
         */
        float graph_size = graph.GetComponent<GraphScript>().graph_size;
        float object_scale = graph.GetComponent<GraphScript>().object_scale;
        graph.transform.localScale = new Vector3(graph_size, graph_size, graph_size);

        Color random_color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        GameObject newData = (GameObject)Instantiate(DataPrefab, graph.transform.position, Quaternion.Euler(0, 0, 0));
        newData.transform.parent = graph.transform;
        newData.GetComponent<DataScript>().SetInputFile(input_file);

        ReadFile(newData);
        newData.GetComponent<DataScript>().GetMaxesAndMins();
        newData.GetComponent<DataScript>().FindXYZIndices();
        float[] maxes = newData.GetComponent<DataScript>().maxes;
        float[] mins = newData.GetComponent<DataScript>().mins;

        int x_col_index = newData.GetComponent<DataScript>().x_col_index;
        int y_col_index = newData.GetComponent<DataScript>().y_col_index;
        int z_col_index = newData.GetComponent<DataScript>().z_col_index;

        graph.GetComponent<GraphScript>().maxes = new Dictionary<string, float>()
        {
            { "x", maxes[x_col_index] },
            { "y", maxes[y_col_index] },
            { "z", maxes[z_col_index] }
        };
        graph.GetComponent<GraphScript>().mins = new Dictionary<string, float>()
        {
            { "x", mins[x_col_index] },
            { "y", mins[y_col_index] },
            { "z", mins[z_col_index] }
        };
        VisualizeData(graph, newData, random_color);
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

                        //Vector3 dataPosition = new Vector3(float.Parse(split_line[6]), float.Parse(split_line[7]), float.Parse(split_line[8]));
                        Vector3 dataPosition = new Vector3(count, count, count);
                        print(dataPosition);
                        //GameObject dataPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        //dataPoint.transform.position = dataPosition;
                        //dataPoint.transform.localScale = new Vector3(OBJECT_SCALE, OBJECT_SCALE, OBJECT_SCALE);

                        //Color random_color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                        //dataPoint.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", random_color);
                        //dataPoint.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");

                        GameObject dataPoint = Instantiate(PlanetPrefab, dataPosition, Quaternion.Euler(0, 0, 0));
                        dataPoint.transform.position = dataPosition;
                        dataPoint.transform.localScale = new Vector3(OBJECT_SCALE, OBJECT_SCALE, OBJECT_SCALE);

                        Color random_color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                        dataPoint.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_EmissionColor", random_color);
                        dataPoint.GetComponent<MeshRenderer>().sharedMaterial.EnableKeyword("_EMISSION");
                        //dataPoint.GetComponent<Material>().
                        //LoadMesh(dataPosition);


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

    public void VisualizeData(GameObject graph, GameObject data, Color sphere_color)
    {
        Dictionary<string, float> graph_mins = graph.GetComponent<GraphScript>().mins;
        Dictionary<string, float> graph_maxes = graph.GetComponent<GraphScript>().maxes;

        float graph_size = graph.GetComponent<GraphScript>().graph_size;
        float object_scale = graph.GetComponent<GraphScript>().object_scale;

        float[] mins = data.GetComponent<DataScript>().mins;
        float[] maxes = data.GetComponent<DataScript>().maxes;

        int file_row_count = data.GetComponent<DataScript>().file_row_count;
        int file_col_count = data.GetComponent<DataScript>().file_col_count;


        int x_col_index = data.GetComponent<DataScript>().x_col_index;
        int y_col_index = data.GetComponent<DataScript>().y_col_index;
        int z_col_index = data.GetComponent<DataScript>().z_col_index;

        float[] scale_diff = new float[file_col_count];

        for (int ind = 0; ind < file_col_count; ind++)
        {
            if (ind == x_col_index)
            {
                scale_diff[ind] = graph_size / (graph_maxes["x"] - graph_mins["x"]);
            }
            else if (ind == y_col_index)
            {
                scale_diff[ind] = graph_size / (graph_maxes["y"] - graph_mins["y"]);
            }
            else if (ind == z_col_index)
            {
                scale_diff[ind] = graph_size / (graph_maxes["z"] - graph_mins["z"]);
            }
            else
            {
                scale_diff[ind] = 1.0f;
            }
        }

        for (int index = 0; index < file_row_count; index++)
        {

            Vector3 new_position = new Vector3(((data.GetComponent<DataScript>().data["x"][index]) - graph_mins["x"]) * scale_diff[x_col_index] + data.transform.position.x,
                ((data.GetComponent<DataScript>().data["y"][index]) - graph_mins["y"]) * scale_diff[y_col_index] + data.transform.position.y,
                ((data.GetComponent<DataScript>().data["z"][index]) - graph_mins["z"]) * scale_diff[z_col_index] + data.transform.position.z);

            GameObject dataPoint = Instantiate(PlanetPrefab);
            dataPoint.GetComponent<Planet>().resolution = 2;
            dataPoint.name = "datapoint_" + index.ToString();

            dataPoint.transform.parent = data.transform;

            dataPoint.transform.position = new_position;
            dataPoint.transform.localScale = new Vector3(OBJECT_SCALE, OBJECT_SCALE, OBJECT_SCALE);

            // Color random_color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            dataPoint.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_EmissionColor", sphere_color);
            dataPoint.GetComponent<MeshRenderer>().sharedMaterial.EnableKeyword("_EMISSION");

            // MUCH SLOWER
            //dataPoint.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", sphere_color);
            //dataPoint.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");


        }
    }


    public void ReadFile(GameObject data)
    {
        /*
         * Loads data from a file and populates a Data object
         * 
         */
        int file_row_count = 0;
        int file_col_count = 0;
        string inputFile = data.GetComponent<DataScript>().input_file;

        List<string> column_names = new List<string>();

        Debug.Log(inputFile);
        try
        {

            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader(inputFile.ToString()))
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
                        for (int col = 0; col < column_names.Count; col++)
                        {
                            // Add column names to data
                            data.GetComponent<DataScript>().AddDataColumn(column_names[col]);
                        }
                    }
                    else
                    {

                        for (int column = 0; column < column_names.Count; column++)
                        {
                            // Add each element to its corresponding column
                            data.GetComponent<DataScript>().AddDataRow(column_names[column], float.Parse(split_line[column]));
                        }

                        // Add to count for every line after the "#" line
                        count++;
                    }
                    file_col_count = column_names.Count;
                }
                file_row_count = count;
            }
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Debug.Log("The file could not be read:");
            Debug.Log(e.Message);

        }

        data.GetComponent<DataScript>().file_col_count = file_col_count;
        data.GetComponent<DataScript>().file_row_count = file_row_count;

        data.GetComponent<DataScript>().PrintAllData();

        graph.GetComponent<GraphScript>().AddData(data);

    }

    public GameObject GetGraphObject()
    {
        return graph;
    }

    public int GetNumFilesLoaded()
    {
        return numFilesLoaded;
    }

    public void LoadMesh(Vector3 position)
    {
        // will make the mesh appear in the scene at origin position
        Graphics.DrawMesh(mesh, position, Quaternion.identity, material, 0);
    }
    public void Update()
    {
        // will make the mesh appear in the scene at origin position
        //Graphics.DrawMesh(mesh, Vector3.zero, Quaternion.identity, material, 0);
    }
}
