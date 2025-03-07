using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class HitPoint : MonoBehaviour
{
    [Header("HP"),SerializeField]
    private int hp, max_hp;
    [Header("HPバーのアニメーター"), SerializeField]
    private Animator hit_point_anim;

    private void Start()
    {
        hp = max_hp;
    }

    [System.Obsolete]
    public void Update()
    {
        //デバッグ用
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Damage();
          
            
        }
    }

    //HPUIをアニメーションで変更
    public void Damage()
    {
        if (hp <= 0) return;
        hp--;
        CameraControll cameraControll = Camera.main.GetComponent<CameraControll>();
        cameraControll.Shake(0.3f);
        hit_point_anim.SetInteger("hit_point", hp);
    }

    public int GetHp()
    {
        return hp;
    }
}
