using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ForgottenForm : MonoBehaviour
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
