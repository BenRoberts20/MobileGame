using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuUIManager : MonoBehaviour
{
    public GameObject[] frames;

    private void Start()
    {

    }
    //Creates only a single instance of the selected scene, and removes the rest.
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }

    //deletes the selected scene
    public void DeleteScene(string SceneName)
    {
        SceneManager.UnloadSceneAsync(SceneName);
    }

    public void OpenFrame(GameObject Frame)
    {
        Time.timeScale = 0;
        for (int i = 0; i < frames.Length; i++)
        {
            if (frames[i] == Frame)
            {
                Frame.SetActive(true);
            }
            else frames[i].SetActive(false);
        }
    }
    public void CloseFrame(GameObject Frame)
    {
        Frame.SetActive(false);
        Time.timeScale = 1;
    }

    //Exits the application in unity and the built version. 
    public void ExitGame()
    {
        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}
