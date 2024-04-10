using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    public LayerMask enemyLayerMask;
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
        Ray        ray = new Ray(transform.position,  transform.forward);
        RaycastHit hit;
        // Debug.DrawLine(transform.position, transform.position+transform.forward, Color.yellow);


        if (Physics.Raycast(ray, out hit, 1000f, enemyLayerMask))
        {
            Debug.DrawLine(transform.position, hit.point, Color.yellow);
            Debug.Log("HitName"+hit.transform.gameObject.name);
            EventManager.instance.EnemyHit(hit.transform.gameObject);
            Handheld.Vibrate();
        }
    }

    private void OnDestroy()
    {
        EventManager.instance.onPlayerShoot -= PlayerShoot;
    }
}