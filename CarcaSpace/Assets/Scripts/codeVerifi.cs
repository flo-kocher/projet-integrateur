using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;


public class codeVerifi : MonoBehaviour
{
    public Button submit;
    public TMP_InputField inputCode;
    // Start is called before the first frame update
    void Start()
    {
        inputCode = GameObject.Find("code").GetComponent<TMP_InputField>();
        //GameObject.Find("Submit").GetComponent<Button>().onClick.AddListener(PostData);
        submit = GameObject.Find("Submit").GetComponent<Button>();
        submit.onClick.AddListener(PostData);
    }

    // Update is called once per frame
    void PostData()
    {
        if(inputCode.text == sendMail.code_verif){
            SceneManager.LoadScene("NewPassword", LoadSceneMode.Single);
        }
        else{
            Debug.Log("code invalide");
        }
    }
}
