using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    [Header("�U��������UI"), SerializeField]
    private RectTransform[] shake_objs;
    [Header("UI�̌����W"),SerializeField]
    private Vector2[] original_positions;
    [Header("�J�����̌����W")]
    private Vector3 original_position;
    [Header("�U������")]
    private float shakeDuration;
    [Header("UI�̐U���̋���"), SerializeField]
    private float ui_shakeMagnitude = 100f;
    [Header("�J�����̐U���̋���"), SerializeField]
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

    // �J�������w�莞�ԁE�����ŗh�炷
    /// <param name = "duration">�h��鎞��</param>
    /// <param name = "magnitude">�h��̋���</param>
    public void Shake(float duration)
    {
        shakeDuration = duration;
    }
}
