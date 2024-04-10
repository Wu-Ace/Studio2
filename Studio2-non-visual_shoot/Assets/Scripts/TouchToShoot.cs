using Manager;
using UnityEngine;

public class TouchToShoot : MonoBehaviour
{
    private bool canShot = true; // 用于跟踪是否已经射击

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            // 当按下触摸屏幕时并且尚未射击时
            if (canShot && Input.touches[0].phase == TouchPhase.Began)
            {
                EventManager.instance.PlayerShoot();
                canShot = false;

                Debug.Log("touch");
            }
            // 当松开触摸屏幕时重置射击状态
            if(!canShot && Input.touches[0].phase == TouchPhase.Ended)
            {
                canShot = true;
            }
        }
    }
}