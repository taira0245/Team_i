using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDirector : MonoBehaviour
{
    enum E_PLState
    {
        Wait,
        Swing,
        CoolTime,

        Max
    }
    [SerializeField] SpritesData pl_sprites;
    [SerializeField] Image pl_image;
    int cr_idx = 0;
    int max_indx = 0;

    private void Awake()
    {
        max_indx = (int)E_PLState.Max - 1;
        cr_idx = 0;
        ChangePLImage((E_PLState)cr_idx);
    }

    private void ChangePLImage(E_PLState type)
    {
        pl_image.sprite = pl_sprites.GetSprite((int)type);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
            if (++cr_idx > max_indx) { cr_idx = 0; }
            ChangePLImage((E_PLState)cr_idx);
        }
    }
}
