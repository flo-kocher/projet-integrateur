using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class CreateGrid : NetworkBehaviour
{
    public int nbTuiles;
    private GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        var list = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach(GameObject i in list)
        {
            if (i.name == "tempGrid")
                go = i;
        }
        for(int x = 0; x<nbTuiles; x++)
        {
            for(int y = 0; y<nbTuiles; y++)
            {
                GameObject clone = GameObject.Instantiate(go);
                clone.transform.SetParent(GameObject.Find("Grid").transform);
                clone.name = x + "/" + y;
                Vector2 v = new Vector2(x+0.5f,y+0.5f);
                clone.transform.position = v;
                clone.SetActive(true);
            }
        }
        Vector3 vec = new Vector3((float)Decimal.Divide(nbTuiles, 2),(float)Decimal.Divide(nbTuiles, -4),-5);
        Camera.main.transform.position = vec;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
