using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;

public class sendMail : MonoBehaviour
{
    string getRouteURI = "localhost:8080/Reset_pass";
    Button submit;
    TMP_InputField inputMail;

    // Start is called before the first frame update
    void Start()
    {
        inputMail = GameObject.Find("email").GetComponent<TMP_InputField>();
        GameObject.Find("Submit").GetComponent<Button>().onClick.AddListener(PostData);
    }

    void PostData() => StartCoroutine(UploadTo());

    public IEnumerator UploadTo(){
        Credentials credentials = new Credentials();
        credentials.mail = inputMail.text;

        string jsonData = JsonUtility.ToJson(credentials, true);
        using(UnityWebRequest restAPI = UnityWebRequest.Put(getRouteURI, jsonData))
        {
            restAPI.method = UnityWebRequest.kHttpVerbPOST;
            restAPI.SetRequestHeader("content-type", "application/json");   
            restAPI.uploadHandler.contentType = "application/json";
            restAPI.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));

            yield return restAPI.SendWebRequest();
            if(restAPI.isNetworkError || restAPI.isHttpError){
                Debug.Log(restAPI.error);
                Debug.Log(jsonData);
            }else{
                Debug.Log("code sent !");
            }
        }

    }

}
