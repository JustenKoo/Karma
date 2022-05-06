// Author: Justen Koo
// Purpose: Destroys the instantiated bullet when it makes contact with another game object

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
