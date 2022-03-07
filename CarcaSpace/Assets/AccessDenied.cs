using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AccessDenied : MonoBehaviour
{
    GameObject face;
    List<Material> m;
    // Start is called before the first frame update
    void Start()
    {
      face = gameObject.transform.GetChild(2).gameObject;
      m = face.GetComponent<Renderer>().materials.ToList();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
