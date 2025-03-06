using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    [SerializeField] private GameObject camera_obj;
    [SerializeField] private int flag;
    [SerializeField] private HitPoint hitPoint; 

    public void FlagPlus()
    {
        flag = 1;
    }

    void Update()
    {
        DamageReaction();
    }

    public void DamageReaction()
    {
        switch (flag)
        {
            case 1:
                goto case 3;
            case 3:
                camera_obj.transform.Translate(30 * Time.deltaTime, 0, 0);
                if (camera_obj.transform.position.x >= 1.0f)
                    flag++;
                break;
            case 2:
                camera_obj.transform.Translate(-30 * Time.deltaTime, 0, 0);
                if (camera_obj.transform.position.x <= -1.0f)
                    flag++;
                break;
            case 4:
                camera_obj.transform.Translate(-30 * Time.deltaTime, 0, 0);
                if (camera_obj.transform.position.x <= 0)
                    flag = 0;
                break;
        }
    }
}
