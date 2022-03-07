using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AccessDenied : MonoBehaviour
{
    GameObject face;
    List<Material> m;
    bool animDenied;
    bool endAnimDenied;
    Material Cross;
    Material Red;
    Material logo;
    // Start is called before the first frame update
    void Start()
    {
      Cross = Resources.Load<Material>("Material/Cross");
      Red = Resources.Load<Material>("Material/Red");
      logo = Resources.Load<Material>("Material/logo");
      face = gameObject.transform.GetChild(2).gameObject;
      m = face.GetComponent<Renderer>().materials.ToList();
    }

    // Update is called once per frame
    void Update()
    {

      if (Input.GetKey("m") && !(animDenied && endAnimDenied))
          animDenied = true;

      if (animDenied)
      {
        m[1] = Red;
        face.GetComponent<Renderer>().materials = m.ToArray();
      }

    }
}
