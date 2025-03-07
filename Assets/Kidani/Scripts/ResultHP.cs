using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class ResultHP : MonoBehaviour
{
    public Sprite zero;
    public Sprite one;
    public Sprite two;
    public Sprite three;
    private Image image;


    void Start()
    {

        // SpriteRenderer�R���|�[�l���g���擾���܂�
        image = GetComponent<Image>();
    }

    void Update()
    {
        //switch (hitpoint.GetHp())
        switch (ScoreMG.GetHoldHP())
        {
            case 3:
                image.sprite = three;   //HP3�̉摜�\��
                break;
            case 2:
                image.sprite = two;     //HP2�̉摜�\��
                break;
            case 1:
                image.sprite = one;     //HP1�̉摜�\��
                break;
            default:
                image.sprite = zero;    //����ȊO0�\��
                break;
                
        }
    }
}
