using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Manager
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
            }

            instance = this;
        }

        public event Action<GameObject> onEnemyHit;

        public void EnemyHit(GameObject enemy)
        {
            if (onEnemyHit != null)
            {
                onEnemyHit(enemy);
            }
            Debug.Log("EventManagaer:EnemyHit");
        }

        public event Action onPlayerShoot;

        public void PlayerShoot()
        {
            if (onPlayerShoot != null)
            {
                onPlayerShoot();
            }
            Debug.Log("EventManager:PlayerShoot");
        }
        public event Action<AudioClip, float> onPlaySound;

        public void PlaySound(AudioClip clip, float volume)
        {
            onPlaySound?.Invoke(clip, volume);
        }

        public event Action<AudioClip, float> onPlayerDie;
        public void PlayerDie(AudioClip clip,float volume)
        {
            if (onPlayerDie != null)
            {
                onPlayerDie(clip,volume);
            }
        }

        public event Action<AudioClip, float> onPlayerHurt;

        public void PlayerHurt(AudioClip clip, float volume)
        {
            if (onPlayerHurt != null)
            {
                onPlayerHurt(clip, volume);
            }
        }

    }
}