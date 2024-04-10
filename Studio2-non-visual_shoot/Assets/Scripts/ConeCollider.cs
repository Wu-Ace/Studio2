using System.Collections;
using UnityEngine;
using System;

public class ConeCast : MonoBehaviour {
    public                   float     radius   = 5;
    public                   float     angle    = 30;
    // public                   float     distance = 10;
    public                   LayerMask enemyLayerMask; // 用于过滤敌人的LayerMask
    [SerializeField] private AudioClip _clip;
    // public                   float     exponent = 2.0f; // 设置指数值

    void Start() {
        StartCoroutine(DetectSound());
    }

    IEnumerator DetectSound() {
        while(true){
            Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayerMask);


            foreach(Collider hit in hits){
                Vector3 direction = hit.transform.position - transform.position;
                float   distanceToObject = direction.magnitude;
                direction = direction.normalized;
                float angleToObject = Vector3.Angle(transform.forward, direction);

                if(angleToObject <= angle){
                    // Debug.Log("Object detected! Distance: " + distanceToObject + " Angle: " + angleToObject);
                    float volume = 1 - (angleToObject / angle);
                    // Debug.Log(volume);
                    SoundManager.instance.PlaySound(_clip,volume);
                }
            }
            yield return new WaitForSeconds(1);
        }
    }
}