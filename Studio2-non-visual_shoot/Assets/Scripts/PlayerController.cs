using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    public  LayerMask enemyLayerMask;
    public  int       bulletMag = 10;
    public  int       currentBullet;
    private float     old_y = 0;
    private float     new_y;
    private float     currentDistance = 0;
    [SerializeField] private AudioClip _shootClip;
    [SerializeField] private AudioClip _reloadClip;
    [SerializeField] private AudioClip _emptyClip;

    public void Start()
    {
        EventManager.instance.onPlayerShoot += PlayerShoot;
        currentBullet = bulletMag;
    }

    public void Update()
    {
        // Debug.DrawLine(transform.position, transform.position + transform.forward * 10, Color.yellow);
        CheckIfReload();
    }

    public void PlayerShoot()
    {
        Ray        ray = new Ray(transform.position,  transform.forward);
        RaycastHit hit;
        // Debug.DrawLine(transform.position, transform.position+transform.forward, Color.yellow);
        if (currentBullet>0)
        {
            SoundManager.instance.PlaySound(_shootClip, 1);
            currentBullet --;
        }
        else
        {
            SoundManager.instance.PlaySound(_emptyClip, 1);
        }

        Debug.Log(currentBullet);
        if (Physics.Raycast(ray, out hit, 1000f, enemyLayerMask)&&currentBullet>0)
        {
            Debug.DrawLine(transform.position, hit.point, Color.yellow);
            Debug.Log("HitName"+hit.transform.gameObject.name);
            EventManager.instance.EnemyHit(hit.transform.gameObject);
            Handheld.Vibrate();
        }
    }

    public void CheckIfReload()
    {

        new_y           = Input.acceleration.y;
        currentDistance = new_y - old_y;
        old_y           = new_y;

        if (currentDistance > 0.5f && Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            Debug.Log("Reload");
            currentBullet = bulletMag;
            SoundManager.instance.PlaySound(_reloadClip, 1);
        }
    }

    private void OnDestroy()
    {
        EventManager.instance.onPlayerShoot -= PlayerShoot;
    }
}