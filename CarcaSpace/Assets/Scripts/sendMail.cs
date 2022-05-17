using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class sendMail : MonoBehaviour
{
    string getRouteURI = "localhost:8080/Reset_pass";
    public Button submit;
    public TMP_InputField inputMail;

    public static string code_verif;
    public static string mail_databsase;
    public Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        inputMail = GameObject.Find("email").GetComponent<TMP_InputField>();
        //GameObject.Find("Submit").GetComponent<Button>().onClick.AddListener(PostData);
        submit = GameObject.Find("Submit").GetComponent<Button>();
        submit.onClick.AddListener(PostData);
        //submit.onClick.AddListener(PostData);
        backButton = GameObject.Find("Back").GetComponent<Button>();
        backButton.onClick.AddListener(returnPage);
    }

    void PostData() => StartCoroutine(UploadTo());

    public void returnPage(){
        SceneManager.LoadScene("GameMenu", LoadSceneMode.Single);
    }
    public IEnumerator UploadTo(){
        mail_databsase = inputMail.text;
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
                Debug.Log(restAPI.downloadHandler.text);
                code_verif = restAPI.downloadHandler.text;
                SceneManager.LoadScene("codeVerification", LoadSceneMode.Single);
            }
        }

    }

}
