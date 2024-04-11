using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    public LayerMask enemyLayerMask;
    public int       bulletMag = 10;
    public int       currentBullet;
    public int       Health = 3;
    public int       PlayerKillEnemyNum;


    private float     old_y = 0;
    private float     new_y;
    private float     currentDistance = 0;

    public bool isLose;


    [SerializeField] private AudioClip _shootClip;
    [SerializeField] private AudioClip _reloadClip;
    [SerializeField] private AudioClip _emptyClip;
    [SerializeField] private AudioClip _dieClip;
    [SerializeField] private AudioClip _aimClip;

    public void Start()
    {
        EventManager.instance.onPlayerShoot += PlayerShoot;
        currentBullet                       =  bulletMag;
        PlayerKillEnemyNum                  =  0;
        isLose                              =  false;
    }

    public void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * 10, Color.yellow);
        CheckIfReload();
        CheckHealth();
        CheckIfAim();
    }

    public void PlayerShoot()
    {
        Ray        ray = new Ray(transform.position,  transform.forward);
        RaycastHit hit;
        // Debug.DrawLine(transform.position, transform.position+transform.forward, Color.yellow);
        if (currentBullet>0)
        {
            SoundManager.instance.PlaySound(_shootClip, 1);
            if (Physics.Raycast(ray, out hit, 1000f, enemyLayerMask)&&currentBullet>0)
            {
                Debug.DrawLine(transform.position, hit.point, Color.yellow);
                Debug.Log("HitName"+hit.transform.gameObject.name);
                EventManager.instance.EnemyHit(hit.transform.gameObject);
                Handheld.Vibrate();
            }
            currentBullet --;
        }
        else
        {
            EventManager.instance.PlaySound(_emptyClip, 1);
        }

        Debug.Log(currentBullet);

    }

    public void CheckIfAim()
    {
        Ray        ray = new Ray(transform.position,  transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 1000f, enemyLayerMask))
        {
            EventManager.instance.PlaySound(_aimClip, 1);
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
            EventManager.instance.PlaySound(_reloadClip, 1);
        }
    }

    public void CheckHealth()
    {
        if (Health <= 0&&!isLose)
        {
            EventManager.instance.PlayerDie(_dieClip,0.7f);
            isLose = true;
        }
    }

    private void OnDestroy()
    {
        EventManager.instance.onPlayerShoot -= PlayerShoot;
    }
}