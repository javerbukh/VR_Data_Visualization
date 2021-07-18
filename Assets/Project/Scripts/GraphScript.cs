using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphScript : MonoBehaviour
{

    public List<GameObject> dataCollection;
    public static int num_data = 0;
    public float graph_size = 20.0f;
    public float object_scale = 0.1f;

    public GameObject Block;

    //public float[] mins;
    //public float[] maxes;

    public Dictionary<string, float> maxes = new Dictionary<string, float>()
    {
        { "x", 0.0f },
        { "y", 0.0f },
        { "z", 0.0f }
    };

    public Dictionary<string, float> mins = new Dictionary<string, float>()
    {
        { "x", float.MaxValue },
        { "y", float.MaxValue },
        { "z", float.MaxValue }
    };


    // Use this for initialization
    void Start()
    {
        dataCollection = new List<GameObject>();
    }

    public void AddData(GameObject data)
    {
        dataCollection.Add(data);
        num_data++;
    }


    public static int GetNumData()
    {
        return num_data;
    }

    public float GetGraphSize()
    {
        return graph_size;
    }

    public void CreateAxes(int num_axes)
    {
        for (float x = 0; x < graph_size; x=x+(graph_size/ num_axes))
        {
            Instantiate(Block, new Vector3(x, 0, 0), Quaternion.identity);

        }
        for (float y = 0; y < graph_size; y=y+(graph_size / num_axes))
        {
            Instantiate(Block, new Vector3(0, y, 0), Quaternion.identity);

        }
        for (float z = 0; z < graph_size; z=z+(graph_size / num_axes))
        {
            Instantiate(Block, new Vector3(0, 0, z), Quaternion.identity);

        }
    }


}
