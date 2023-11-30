using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public GameObject warningFrame;

    //Creates only a single instance of the selected scene, and removes the rest.
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }

    //Adds a scene to the existing scene
    public void LoadSceneIntoCurrentScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);

    }

    //deletes the selected scene
    public void DeleteScene(string SceneName)
    {
        SceneManager.UnloadSceneAsync(SceneName);
    }

    //prompts a warning, which asks you if you want to change scene or not.
    public void ToggleWarning(bool value)
    {
        warningFrame.SetActive(value);
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
