using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class ForgottenForm : MonoBehaviour
{

    string getRouteURI = "http://185.155.93.105:11007/UpdateUserPassword";
    public Button submit;
    public TMP_InputField inputPass;
    public TMP_InputField inputPassConfirm;


    // Start is called before the first frame update
    void Start()
    {
        inputPass = GameObject.Find("password").GetComponent<TMP_InputField>();
        inputPassConfirm = GameObject.Find("confirm").GetComponent<TMP_InputField>();
        
        //GameObject.Find("Submit").GetComponent<Button>().onClick.AddListener(PostData);
        
        submit = GameObject.Find("Submit").GetComponent<Button>();
        submit.onClick.AddListener(PostData);
        
        //submit.onClick.AddListener(PostData);
    }

    void PostData() => StartCoroutine(UploadTo());

    public IEnumerator UploadTo(){
        Debug.Log(inputPass.text);
        Debug.Log(inputPassConfirm.text);
        if(String.Equals(inputPass.text, inputPassConfirm.text)){
            Credentials credentials = new Credentials();
            credentials.pass = inputPass.text;
            credentials.mail = sendMail.mail_databsase;
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
                    Debug.Log(jsonData);
                }else{
                    Debug.Log("done!!");
                    Debug.Log(restAPI.downloadHandler.text);
                    SceneManager.LoadScene("GameMenu", LoadSceneMode.Single);
                }
            }
        }
        else{
            Debug.Log("passwords are not the same");
        }
        

    }

}
