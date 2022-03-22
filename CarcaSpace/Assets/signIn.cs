using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;

[Serializable]
public class Credentials
{
    public string name;
    public string mail;
    public string pass;
}
public class signIn : MonoBehaviour{
    TMP_InputField inputUsername;
    TMP_InputField inputPassword;
    TMP_InputField inputMail;
    Button submit;

    string getRouteURI = "localhost:3000/signIn";
    void Start(){
        inputUsername = GameObject.Find("Username").GetComponent<TMP_InputField>();
        inputPassword = GameObject.Find("Password").GetComponent<TMP_InputField>();
        inputMail = GameObject.Find("Mail").GetComponent<TMP_InputField>();
        GameObject.Find("Submit").GetComponent<Button>().onClick.AddListener(PostData);
        
    }
    void PostData() => StartCoroutine(Upload());

    public IEnumerator Upload(){
        Credentials credentials = new Credentials();
        credentials.name = inputUsername.text;
        credentials.pass = inputPassword.text;
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
                Debug.Log(jsonData);
                Debug.Log(restAPI.error);
            }else{
                Debug.Log("Form uploaded !");
            }
        }
    }
}