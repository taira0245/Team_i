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

    // ƒJƒƒ‰‚ğw’èŠÔE‹­‚³‚Å—h‚ç‚·
    /// <param name = "duration">—h‚ê‚éŠÔ</param>
    /// <param name = "magnitude">—h‚ê‚Ì‹­‚³</param>
    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}
