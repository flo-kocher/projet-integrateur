using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
     var list = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject i in list)
        {
            if (i.name.Contains("Pioche"))
                i.GetComponent<Move>().enabled = false;
        }
  
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      var list = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject i in list)
        {
            if (i.name.Contains("Pioche"))
                i.GetComponent<Move>().enabled = true;
        }
    }


}
