using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] bool loadOnStart;
    [SerializeField] string target;

    void Start()
    {
        if (loadOnStart)
        {
            LoadScene(target);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
