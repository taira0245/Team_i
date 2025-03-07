using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultText : MonoBehaviour
{
    public HitPoint hitpoint; // インスペクターでアタッチ

    public Sprite newSprite;
    private Image image;


    void Start()
        {
        
            // SpriteRendererコンポーネントを取得します
            image = GetComponent<Image>();
        }

        void Update()
        {

        if (ScoreMG.GetHoldHP() <= 0)    //もし失敗していたら失敗画像に変える
            {
                // 画像を切り替えます
                image.sprite = newSprite;
            }
        }
    }
