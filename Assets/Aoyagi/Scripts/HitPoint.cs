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

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
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
        cameraControll.Shake(0.3f, 0.5f);
        hit_point_anim.SetInteger("hit_point", hp);
    }

    public int GetHp()
    {
        return hp;
    }
}
