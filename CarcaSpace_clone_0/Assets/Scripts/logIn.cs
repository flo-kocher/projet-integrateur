using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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

[Serializable]

public class serverResponse {
    public string success;
    public string message;
    public string error;
}
public class logIn : MonoBehaviour{
    TMP_InputField inputUsername;
    TMP_InputField inputPassword;
    Button submit;
    Text serverResponseText; 



    string getRouteURI = "localhost:8080/logIn";
    string success = "{\"success\": true}";
    void Start(){
        inputUsername = GameObject.Find("Username").GetComponent<TMP_InputField>();
        inputPassword = GameObject.Find("Password").GetComponent<TMP_InputField>();
        submit = GameObject.Find("Submit").GetComponent<Button>();
        serverResponseText = GameObject.Find("Response").GetComponent<Text>();

        if(inputUsername.text != null && inputPassword.text != null){
            submit.onClick.AddListener(PostData);
        }
    }
    void PostData() => StartCoroutine(Upload());
    public IEnumerator Upload(){

        Credentials credentials = new Credentials();
        credentials.name = inputUsername.text;
        credentials.pass = inputPassword.text;

        string jsonData = JsonUtility.ToJson(credentials, true);
        using(UnityWebRequest restAPI = UnityWebRequest.Put(getRouteURI, jsonData))
        {
            restAPI.method = UnityWebRequest.kHttpVerbPOST;
            restAPI.SetRequestHeader("content-type", "application/json;charset=utf-8");
            restAPI.uploadHandler.contentType = "application/json";
            restAPI.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            restAPI.downloadHandler = new DownloadHandlerBuffer();

            yield return restAPI.SendWebRequest();
            if(restAPI.result == UnityWebRequest.Result.ProtocolError){
                Debug.Log(restAPI.error);
                Debug.Log(jsonData);
                Debug.Log(restAPI.downloadHandler.error);
            }else{
                Debug.Log("Form uploaded !");
                Debug.Log(restAPI.downloadHandler.text);
                serverResponse data = JsonUtility.FromJson<serverResponse>(restAPI.downloadHandler.text);
               
                if(data.success == "true")
                {
                    SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
                }
                else if(data.success == "false" && data.message == "Not existing user") {
                    serverResponseText.text = "Wrong credentials";
                }
                else if(data.error != null && data.success == "false") {
                    Debug.Log("Fatal error");
                }
                else {
                    Debug.Log("Erreur inconnue");
                }
            }
        }

      
    }
}
