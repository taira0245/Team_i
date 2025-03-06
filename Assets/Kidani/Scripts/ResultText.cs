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
        
            // SpriteRendererコンポーネントを取得します
            image = GetComponent<Image>();
        }

        void Update()
        {

        if (hp <= 0)    //もし失敗していたら失敗画像に変える
            {
                // 画像を切り替えます
                image.sprite = newSprite;
            }
        }
    }
