using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class signIn : MonoBehaviour{
    TMP_InputField inputUsername;
    TMP_InputField inputPassword;
    TMP_InputField inputMail;
    Button submit;
    
    string getRouteURI = "http://185.155.93.105:11007/signIn";
    void Start(){
        inputUsername = GameObject.Find("Username").GetComponent<TMP_InputField>();
        inputPassword = GameObject.Find("Password").GetComponent<TMP_InputField>();
        inputMail = GameObject.Find("Mail").GetComponent<TMP_InputField>();
        GameObject.Find("Submit").GetComponent<Button>().onClick.AddListener(PostData);
        
    }
    void PostData() => StartCoroutine(UploadTo());

    public IEnumerator UploadTo(){

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
            if(restAPI.result != UnityWebRequest.Result.Success){
                Debug.Log(restAPI.error);
                Debug.Log(restAPI.result);
                Debug.Log(jsonData);
            }else{
                Debug.Log("Form uploaded !");
                Debug.Log(restAPI.result);
                SceneManager.LoadScene("GameMenu", LoadSceneMode.Single);
            }
        }
    }
}