using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

[Serializable]
public class playerStats{
    [SerializeField] private string playerName;
    public string getPlayerName() {
        return this.playerName;
    }

    public string setPlayerName(string name) {
        return this.playerName = name;
    }
}

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

    public string name;
    public string error;
}


public class logIn : MonoBehaviour{
    TMP_InputField inputUsername;
    TMP_InputField inputPassword;
    Button submit;
    Text serverResponseText; 
    playerStats playerStat ; 
    string PlayerName ;


    string getRouteURI = "http://185.155.93.105:11007/logIn";
    void Start(){
        playerStat = new playerStats();
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
            if(restAPI.result != UnityWebRequest.Result.Success){
                Debug.Log(restAPI.error);
                Debug.Log(jsonData);
                Debug.Log(restAPI.downloadHandler.error);
            }else{
                Debug.Log("request uploaded !");
                Debug.Log(restAPI.downloadHandler.text);
                serverResponse data = JsonUtility.FromJson<serverResponse>(restAPI.downloadHandler.text);
               
                if(data.success == "true")
                {
                    playerStat.setPlayerName(data.name);
                    PlayerName =  playerStat.getPlayerName();
                    PlayerPrefs.SetString("playerName", PlayerName);
                    PlayerPrefs.Save ();
                    SceneManager.LoadScene("CreateOrJoin", LoadSceneMode.Single);
                }
                else if(data.success == "false") {
                    serverResponseText.text = data.message;
                }
                else if(data.error != null && data.success == "false") {
                    serverResponseText.text = "Fatal error";
                }
                else {
                   serverResponseText.text = "Unknown error";
                }
            }
        }      
    }


}
