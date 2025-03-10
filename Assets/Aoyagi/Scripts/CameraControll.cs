using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    private Vector3 originalPosition;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.1f;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = originalPosition + (Vector3)(Random.insideUnitCircle * shakeMagnitude);
            shakeDuration -= Time.deltaTime;
        }
        else
        {
            transform.localPosition = originalPosition;
        }
    }

    // カメラを指定時間・強さで揺らす
    /// <param name = "duration">揺れる時間</param>
    /// <param name = "magnitude">揺れの強さ</param>
    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}
