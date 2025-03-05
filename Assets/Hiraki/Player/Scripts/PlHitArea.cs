using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlHitArea : MonoBehaviour
{
    [SerializeField] Image area_image;
    [Tooltip("�q�b�g����\��������͈͓��ɒe������Ƃ��̃G���A�̐F")]
    [SerializeField] Color in_hit_color = Color.yellow;
    Color default_color = Color.white;

    private void Awake()
    {
        default_color = area_image.color;
    }

    /// <summary>
    /// �q�b�g�̉\�������邩�̊m�F
    /// </summary>
    /// <returns></returns>
    bool CheckHitable()
    {
        bool flag = false;
        return flag;
    }

    void HitAreaUpdate()
    {
        if (CheckHitable()) {
            area_image.color = in_hit_color;
        }
        else {
            area_image.color = default_color;
        }
    }

    private void Update()
    {
        //�q�b�g�\���̏���
        HitAreaUpdate();
    }
}
