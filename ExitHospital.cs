// Author: Justen Koo
// Purpose: Scene management for exiting the hospital and entering the city

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitHospital : MonoBehaviour
{
    MenuManager manager;
    public void Start()
    {
        manager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        /*Debug.LogError("Entering City");
        SceneManager.LoadScene("City", LoadSceneMode.Single);*/
        Debug.Log("Trigger Enter");
        Time.timeScale = 0; // Freeze Game
        manager.DemoCompleteScreen();
    }
}
