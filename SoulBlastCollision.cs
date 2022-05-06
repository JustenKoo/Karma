// Author: Justen Koo
// Purpose: Handles collisions for the player's instantiated soul blast

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBlastCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
