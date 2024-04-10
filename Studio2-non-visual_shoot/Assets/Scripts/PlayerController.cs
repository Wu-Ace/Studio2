using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public void Start()
    {
        EventManager.instance.onPlayerShoot += PlayerShoot;
    }

    public void Update()
    {
        // Debug.DrawLine(transform.position, transform.position + transform.forward * 10, Color.yellow);
    }

    public void PlayerShoot()
    {
        Ray        ray = new Ray(transform.position, transform.position+ transform.forward*10);
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