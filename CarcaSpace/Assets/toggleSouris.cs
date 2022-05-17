using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleSouris : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      GetComponent<Toggle>().onValueChanged.AddListener(delegate {
                ToggleValueChanged(GetComponent<Toggle>());
            });

    }

    void ToggleValueChanged(Toggle change)
    {
      Camera.main.GetComponent<CameController>().souris = !Camera.main.GetComponent<CameController>().souris;
    }
}
