using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultText : MonoBehaviour
{
    public HitPoint hitpoint; // �C���X�y�N�^�[�ŃA�^�b�`

    public Sprite newSprite;
    private Image image;


    void Start()
        {
        
            // SpriteRenderer�R���|�[�l���g���擾���܂�
            image = GetComponent<Image>();
        }

        void Update()
        {

        if (ScoreMG.GetHoldHP() <= 0)    //�������s���Ă����玸�s�摜�ɕς���
            {
                // �摜��؂�ւ��܂�
                image.sprite = newSprite;
            }
        }
    }
