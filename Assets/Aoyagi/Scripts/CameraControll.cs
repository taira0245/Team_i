using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    [Header("振動させるUI"), SerializeField]
    private RectTransform[] shake_objs;
    [Header("UIの元座標"),SerializeField]
    private Vector2[] original_positions;
    [Header("カメラの元座標")]
    private Vector3 original_position;
    [Header("振動時間")]
    private float shakeDuration;
    [Header("UIの振動の強さ"), SerializeField]
    private float ui_shakeMagnitude = 100f;
    [Header("カメラの振動の強さ"), SerializeField]
    private float camera_shakeMagnitude = 0.5f;

    void Start()
    {
        shakeDuration = 0;
        original_position = transform.localPosition;
        for (int i = 0; i < shake_objs.Length; i++)
        {
            original_positions[i] = shake_objs[i].anchoredPosition;
        }
    }

    void Update()
    {
        ShakeMove();
    }

    public void ShakeMove()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = original_position + (Vector3)(Random.insideUnitCircle * camera_shakeMagnitude);
            for (int i = 0; i < shake_objs.Length; i++)
            {
                shake_objs[i].anchoredPosition = original_positions[i] + Random.insideUnitCircle * ui_shakeMagnitude;
            }
            shakeDuration -= Time.deltaTime;
        }
        else
        {
            transform.localPosition = original_position;
            for (int i = 0; i < shake_objs.Length; i++)
            {
                shake_objs[i].anchoredPosition = original_positions[i];
            }
        }
    }

    // カメラを指定時間・強さで揺らす
    /// <param name = "duration">揺れる時間</param>
    /// <param name = "magnitude">揺れの強さ</param>
    public void Shake(float duration)
    {
        shakeDuration = duration;
    }
}
