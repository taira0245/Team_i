using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_2 : MonoBehaviour
{
    [Header("ŠÔ"), SerializeField]
    private float current_time, max_time;
    [Header("•bj"), SerializeField]
    private Image time_arrow;
    [Header("1•b‚ ‚½‚è‚Ì‰ñ“]Šp“x"), SerializeField]
    private float roll_speed;

    private void Start()
    {
        current_time = max_time;
    }

    public void Update()
    {
        TimeRoll();
        GameEnd();
    }

    public void TimeRoll()
    {
        if (current_time <= 0) return;
        current_time -= Time.deltaTime;
        float angle = current_time * roll_speed;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void GameEnd()
    {
        if (current_time <= 0)
        {
            Debug.Log("ƒ^ƒCƒ€ƒAƒbƒv");
        }
    }
}
