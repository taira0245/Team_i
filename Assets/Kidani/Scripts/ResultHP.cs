using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class ResultHP : MonoBehaviour
{
    [SerializeField] private int hp = 3;


    public Sprite zero;
    public Sprite one;
    public Sprite two;
    public Sprite three;
    private Image image;


    void Start()
    {

        // SpriteRendererコンポーネントを取得します
        image = GetComponent<Image>();
    }

    void Update()
    {
        switch (hp)
        {
            case 3:
                image.sprite = three;   //HP3の画像表示
                break;
            case 2:
                image.sprite = two;     //HP2の画像表示
                break;
            case 1:
                image.sprite = one;     //HP1の画像表示
                break;
            default:
                image.sprite = zero;    //それ以外0表示
                break;
                
        }
    }
}
