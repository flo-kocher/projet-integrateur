using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PIck : MonoBehaviour
{
    private static int compteur = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(func);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void func()
    {
        GameObject temp = null;
        var list = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach(GameObject i in list)
        {
            if (i.name == "Temp")
                temp = i;
            if (i.name.Contains("Pioche"))
                    i.GetComponent<Move>().enabled = false;
        }
        GameObject clone = GameObject.Instantiate(temp);
        clone.SetActive(true);
        clone.transform.SetParent(GameObject.Find("Tiles").transform);
        compteur++;
        clone.name = "Pioche" + compteur;
    }
}
