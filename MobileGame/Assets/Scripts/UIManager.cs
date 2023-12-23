using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject warningFrame;
    public TMP_Text HealthTxt;
    public TMP_Text DamageTxt;
    private Player plr;
    public GameObject[] frames;
    public GameObject[] ShipButtons;

    private void Start()
    {
        plr = GameObject.Find("Player").GetComponent<Player>();
    }
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
        if (Frame.name == "StatsGUI") LoadStatsInfo();
        if (Frame.name == "ShipsGUI") CheckForOwnedShips();
    }
    public void CloseFrame(GameObject Frame)
    {
        Frame.SetActive(false);
        frames[0].SetActive(true);
        Time.timeScale = 1;
    }

    public void SpeedUpTime()
    {
        GameObject tog = GameObject.Find("SpeedToggle");
        if (tog.GetComponent<Toggle>().isOn) Time.timeScale = 2;
        else if (!tog.GetComponent<Toggle>().isOn) Time.timeScale = 1;
    }

    //Exits the application in unity and the built version. 
    public void ExitGame()
    {
        GameObject.Find("Player").GetComponent<Player>().SaveData();
        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

    public void IncreaseStat(string Stat)
    {
        if (Stat == "Health") plr.IncreaseHealth(2);
        if (Stat == "Damage") plr.IncreaseDamage(1);
        LoadStatsInfo();
        plr.SaveData();
    }
    private void LoadStatsInfo()
    {
        HealthTxt.text = "Health: " + plr.GetHealth().ToString();
        DamageTxt.text = "Damage: " + plr.GetDamage().ToString();
        GameObject.Find("PointsTxt").gameObject.GetComponent<TMP_Text>().text = "Points: " + plr.GetPoints();
    }

    private void CheckForOwnedShips()
    {
        for (int i = 0; i < ShipButtons.Length; i++)
        {
            if (ShipButtons[i].name != "StarterShip")
            {
                if(PlayerPrefs.HasKey(ShipButtons[i].name))
                {
                    ShipButtons[i].SetActive(true);
                }
                else ShipButtons[i].SetActive(false);
            }
        }
    }

    public void ChangeShip(Ships Ship)
    {
        plr.LoadShipData(Ship);
        CloseFrame(GameObject.Find("ShipsGUI"));
    }
}
