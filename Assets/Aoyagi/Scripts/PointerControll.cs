using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerControll : MonoBehaviour
{
    [Header("Canvas�̎Q��"), SerializeField]
    private Canvas canvas;
    [Header("Canvas�̈ʒu"), SerializeField]
     private RectTransform canvas_transform;
    [Header("�}�E�X�|�C���^�[�̈ʒu"),SerializeField]
    private RectTransform cursor_transform;

    void Update()
    {
        Tracking();
    }

    public void Tracking()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
           canvas_transform,
           Input.mousePosition,
           canvas.worldCamera,
           out var mousePosition);

        cursor_transform.anchoredPosition = new Vector2(mousePosition.x, mousePosition.y);
    }
}
