using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private int _sceneNumber;
    public void ChangeScene(int sceneNumber)
    {
        _sceneNumber = sceneNumber;
        SceneManager.LoadScene(_sceneNumber);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}