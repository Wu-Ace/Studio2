using UnityEngine;
using System.Collections;
public class ConeCast : MonoBehaviour {
    public float radius = 5;
    public float angle = 30;
    public float distance = 10;
    public LayerMask layerMask;

    void Update(){
        Vector3 origin = transform.position;
        RaycastHit[] hits = Physics.SphereCastAll(origin, radius,
            transform.forward, distance, layerMask);

        foreach(RaycastHit hit in hits){
            Vector3 direction = hit.point - origin;
            float distanceToObject = direction.magnitude;
            direction = direction.normalized;
            float angleToObject = Vector3.Angle(transform.forward, direction);

            if(angleToObject <= angle){
                Debug.Log("Object detected! Distance: " + distanceToObject + " Angle: " + angleToObject);
            }
        }
    }
}