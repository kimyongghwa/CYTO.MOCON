using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionController : MonoBehaviour
{
    public void goMain()
    {
        SceneManager.LoadScene(0);
    }
    public void Exixit()
    {
        Application.Quit();
    }
}
