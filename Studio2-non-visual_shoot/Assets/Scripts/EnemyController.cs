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
    private void Update()
    {
        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed*Time.deltaTime);
    }

    private void EnemyBeingHurt(GameObject enemy)
    {
        if (enemy == this.gameObject)
        {
            Debug.Log("Enemy is being hurt");
            Destroy(this.gameObject);
            EnemySpawner.EnemyNumber--;
        }
        SoundManager.instance.PlaySound(DefeatedClip, 1);
        Debug.Log("EnemyController:EnemyBeingHurt");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController.Health--;
            Debug.Log("Player Health: " + PlayerController.Health);
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        EventManager.instance.onEnemyHit -= EnemyBeingHurt;
    }
}