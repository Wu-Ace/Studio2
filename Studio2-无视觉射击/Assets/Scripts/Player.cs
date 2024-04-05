using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;


public class Player : MonoBehaviour
{
    public void Start()
    {
        EventManager.instance.onPlayerShoot += PlayerShoot;
    }
    public void PlayerShoot()
    {
        Ray        ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.yellow);
            EventManager.instance.EnemyHit(hit.transform.gameObject);
            Handheld.Vibrate();
        }
    }

    private void OnDestroy()
    {
        EventManager.instance.onPlayerShoot -= PlayerShoot;
    }
}