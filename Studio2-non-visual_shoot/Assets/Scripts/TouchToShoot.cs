using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class TouchToShoot : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            EventManager.instance.PlayerShoot();
            // Debug.Log("touch");
        }
    }
}