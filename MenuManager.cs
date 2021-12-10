using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml;
using TMPro;

public class MenuManager : MonoBehaviour
{
    //Menus and Screens
    public GameObject MainMenuCanvas;
    public GameObject SettingsCanvas;
    public GameObject ControlsSettingsMenu;
    public GameObject VideoSettingsMenu;
    public GameObject AudioSettingsMenu;
    public GameObject EscCanvas;
    public List<GameObject> MenuList = new List<GameObject>();
    //XML for Missions
    public TextMeshProUGUI currentMissionText;
    public TextMeshProUGUI missionText;
    public Transform missionContainer;
    public XmlDocument missionDataXml;

    public void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        MenuList.Add(MainMenuCanvas);
        MenuList.Add(SettingsCanvas);
        MenuList.Add(ControlsSettingsMenu);
        MenuList.Add(VideoSettingsMenu);
        MenuList.Add(AudioSettingsMenu);
        MenuList.Add(EscCanvas);
        Scene currScene = SceneManager.GetActiveScene();
        if (currScene.name == "MainMenu")
        {
            Debug.Log(MainMenuCanvas);
            SetCurrentMenu(MainMenuCanvas);
        }
        else
        {
            HideAllScreens();
        }

        //missionText = new TextMeshProUGUI();
        //currentMissionText = new TextMeshProUGUI();
        TextAsset xmlTextAsset =  (TextAsset) Resources.Load("Missions");
        missionDataXml = new XmlDocument();
        missionDataXml.LoadXml(xmlTextAsset.text);
    }
    public void ShowCurrentMissions(XmlNode curMissionNode)
    {
        //This is bad code
        XmlNodeList missions = missionDataXml.SelectNodes("/GameEvents/Mission");

        for (int i = 0; i < missions.Count; i++)
        {
            if (i == 0)
            {
                missionText.SetText(missions[i].ChildNodes[0].InnerText);
            }
            else
            {
                currentMissionText.SetText(missions[i].ChildNodes[0].InnerText);
            }
        }

        //foreach (XmlNode item in missions)
        //{
            //GameObject newMissionUI = GameObject.Instantiate(currentMissionText, missionContainer);
            //Debug.Log(item.ChildNodes[0].InnerText);
        //}
    }

    public void ReturnToGame()
    {
        foreach (Transform t in missionContainer)
        {
            Destroy(t.gameObject);
        }
        for (int i = 0; i < MenuList.Count; i++)
        {
            MenuList[i].SetActive(false);
        }
    }
    public void HideAllScreens()
    {
        for (int i = 0; i < MenuList.Count; i++)
        {
            MenuList[i].SetActive(false);
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SetCurrentMenu(GameObject currentMenu)
    {
        Debug.Log(currentMenu);
        for (int i = 0; i < MenuList.Count; i++)
        {
            MenuList[i].SetActive(false);
        }
        currentMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    //Buttons for the Main Menu
    public void ContinueButton()
    {
        SceneManager.LoadScene("TestRoomJK");
    }
    public void NewGameButton()
    {
        SceneManager.LoadScene("City");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void SettingsButton()
    {
        MainMenuCanvas.SetActive(false);
        EscCanvas.SetActive(false);
        SettingsCanvas.SetActive(true);
        ControlsSettingsMenu.SetActive(true);
    }
    public void ExitButton()
    {
        Application.Quit();
    }

    public void EscMenu()
    {
        Debug.Log(EscCanvas);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SetCurrentMenu(EscCanvas);
        ShowCurrentMissions(missionDataXml);
    }

    //Buttons for the Settings Screen
    public void BackToMainMenu()
    {
        SetCurrentMenu(MainMenuCanvas);
    }
    public void ControlsMenu()
    {
        ControlsSettingsMenu.SetActive(true);
        VideoSettingsMenu.SetActive(false);
        AudioSettingsMenu.SetActive(false);
    }
    public void VideoMenu()
    {
        ControlsSettingsMenu.SetActive(false);
        VideoSettingsMenu.SetActive(true);
        AudioSettingsMenu.SetActive(false);
    }
    public void AudioMenu()
    {
        ControlsSettingsMenu.SetActive(false);
        VideoSettingsMenu.SetActive(false);
        AudioSettingsMenu.SetActive(true);
    }
}