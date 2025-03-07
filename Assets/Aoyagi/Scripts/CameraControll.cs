using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    [Header("U“®‚³‚¹‚éUI"), SerializeField]
    private RectTransform[] shake_objs;
    [Header("UI‚ÌŒ³À•W"),SerializeField]
    private Vector2[] original_positions;
    [Header("ƒJƒƒ‰‚ÌŒ³À•W")]
    private Vector3 original_position;
    [Header("U“®ŠÔ")]
    private float shakeDuration;
    [Header("UI‚ÌU“®‚Ì‹­‚³"), SerializeField]
    private float ui_shakeMagnitude = 100f;
    [Header("ƒJƒƒ‰‚ÌU“®‚Ì‹­‚³"), SerializeField]
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

    // ƒJƒƒ‰‚ğw’èŠÔE‹­‚³‚Å—h‚ç‚·
    /// <param name = "duration">—h‚ê‚éŠÔ</param>
    /// <param name = "magnitude">—h‚ê‚Ì‹­‚³</param>
    public void Shake(float duration)
    {
        shakeDuration = duration;
    }
}
