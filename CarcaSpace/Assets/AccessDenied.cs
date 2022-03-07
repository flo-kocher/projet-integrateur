using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessDenied : MonoBehaviour
{
    GameObject face;
    Material[] m;
    // Start is called before the first frame update
    void Start()
    {
      face = gameObject.transform.GetChild(2).gameObject;
      m = face.GetComponent<Renderer>().materials;
      Debug.Log(m[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
