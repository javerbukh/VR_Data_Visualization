using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphScript : MonoBehaviour
{

    public List<GameObject> dataCollection;
    public static int num_data = 0;


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


}
