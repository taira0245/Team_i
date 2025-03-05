using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerControll : MonoBehaviour
{
    [Header("Canvasの参照"), SerializeField]
    private Canvas canvas;
    [Header("Canvasの位置"), SerializeField]
     private RectTransform canvas_transform;
    [Header("マウスポインターの位置"),SerializeField]
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
