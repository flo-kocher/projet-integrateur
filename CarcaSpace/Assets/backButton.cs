using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class backButton : MonoBehaviour , IPointerClickHandler
{

    
  public GameObject back;
  public GameObject raw;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  public void OnPointerClick(PointerEventData eventData) {
    GameObject.Find("Main Camera").GetComponent<Camera>().enabled = true;
    GameObject.Find("DuplicateCam").GetComponent<Camera>().enabled = false;
    raw.SetActive(true);
    back.SetActive(false);
  }
}
