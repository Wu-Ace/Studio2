using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemySpawner EnemySpawner;
    PlayerController PlayerController;

    private void Awake()
    {
        EnemySpawner     = GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        PlayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        EventManager.instance.onEnemyHit += EnemyBeingHurt;
        player                           =  GameObject.FindWithTag("Player").transform;
    }

    public Transform player;     // Reference to the player's position
    public float     speed = 1f; // Speed at which the enemy moves towards the player
    public AudioClip DefeatedClip;
    public AudioClip PlayerHurtClip;
    private void Update()
    {
        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed*Time.deltaTime);
    }

    private void EnemyBeingHurt(GameObject enemy)
    {
        if (enemy == this.gameObject)
        {
            PlayerController.PlayerKillEnemyNum++;
            SoundManager.instance.PlaySound(DefeatedClip, 0.5f);
            Debug.Log("EnemyController:EnemyBeingHurt");
            Debug.Log("Enemy is being hurt");
            EnemySpawner.EnemyNumber--;
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController.Health--;
            EnemySpawner.EnemyNumber--;
            Debug.Log("Player Health: " + PlayerController.Health);
            EventManager.instance.PlayerHurt(PlayerHurtClip,1);
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        EventManager.instance.onEnemyHit -= EnemyBeingHurt;
    }
}