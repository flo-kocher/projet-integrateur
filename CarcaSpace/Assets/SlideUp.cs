using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideUp : MonoBehaviour
{
    public GameObject Button;
    public void startSlider()
    {
       if(Button != null){
           Animator animator = Button.GetComponent<Animator>();
           if(animator != null){
               bool isOpen = animator.GetBool("show");
               animator.SetBool("show", !isOpen);
               Debug.Log("1");
           }
       }
    }
}
