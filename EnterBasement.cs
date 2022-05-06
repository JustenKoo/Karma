// Author: Justen Koo
// Purpose: Handles scene management to enter the basement

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterBasement : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("HospitalBasement");
    }
}
