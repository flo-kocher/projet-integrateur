using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
public class forget_pass_load : MonoBehaviour
{
    Button enterPasswordForm;
    public void PasswordForm()
    {
         SceneManager.LoadScene("ForgotPassword", LoadSceneMode.Single);
    }

    public void LoggedIn()
    {

    }

    void Start(){
        enterPasswordForm = GameObject.Find("Submit1").GetComponent<Button>();
        enterPasswordForm.onClick.AddListener(PasswordForm);
    }
}
