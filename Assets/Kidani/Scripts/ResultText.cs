using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultText : MonoBehaviour
{
    [SerializeField] private int hp = 3;

    public Sprite newSprite;
        private Image image;


    void Start()
        {
        
            // SpriteRenderer�R���|�[�l���g���擾���܂�
            image = GetComponent<Image>();
        }

        void Update()
        {

        if (hp <= 0)    //�������s���Ă����玸�s�摜�ɕς���
            {
                // �摜��؂�ւ��܂�
                image.sprite = newSprite;
            }
        }
    }
