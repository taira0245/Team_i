using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpritesData")]
public class SpritesData : ScriptableObject
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] Sprite no_data_Sprite;

    public Sprite GetSprite(int index = 0) => sprites[index];
    public int Count => sprites.Length;
}
