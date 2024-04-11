using System.Collections;
using UnityEngine;
using System;

public class ConeCast : MonoBehaviour {
    public float radius;
    public float SlowAngle;
    public float FastAngle;
    public float warmRadius;

    // public                   float     distance = 10;
    public                   LayerMask enemyLayerMask; // 用于过滤敌人的LayerMask
    [SerializeField] private AudioClip _slowClip;

    [SerializeField] private AudioClip _warmClip;
    // [SerializeField] private AudioClip _fastClip;
    // public                   float     exponent = 2.0f; // 设置指数值

    void Start() {
        StartCoroutine(DetectSound());
        StartCoroutine(DetectEnemyCome());
    }

    IEnumerator DetectSound() {
        while(true){
            Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayerMask);


            foreach(Collider hit in hits){
                Vector3 direction = hit.transform.position - transform.position;
                float   distanceToObject = direction.magnitude;
                direction = direction.normalized;
                float angleToObject = Vector3.Angle(transform.forward, direction);

                if(FastAngle<angleToObject&&angleToObject <= SlowAngle){
                    // Debug.Log("Object detected! Distance: " + distanceToObject + " Angle: " + angleToObject);
                    float volume = 1 - (angleToObject / SlowAngle);
                    float pitch  = 3 * volume;
                    // Debug.Log(volume);
                    SoundManager.instance.PlaySound(_slowClip,volume);
                }
                // else if(hit.gameObject.tag == "Enemy" ){                    // Debug.Log("Object detected! Distance: " + distanceToObject + " Angle: " + angleToObject);
                //     float volume = 1 - (angleToObject / FastAngle);
                //     float pitch  = 3 * volume;
                //     // Debug.Log(volume);
                //     SoundManager.instance.PlaySound(_fastClip,volume);
                // }
            }
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator DetectEnemyCome() {
        while(true){
            Collider[] hits = Physics.OverlapSphere(transform.position, warmRadius, enemyLayerMask);


            foreach(Collider hit in hits){
                SoundManager.instance.PlaySound(_warmClip,1);
            }
            yield return new WaitForSeconds(1);
        }
    }
}