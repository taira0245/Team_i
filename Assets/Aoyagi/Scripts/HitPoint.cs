using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPoint : MonoBehaviour
{
    [Header("HP"),SerializeField]
    private int hp, max_hp;
    [Header("HPÉoÅ["), SerializeField]
    private Image[] hit_points;

    private void Start()
    {
        hp = max_hp;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Damage();
        }
    }

    public void Damage()
    {
        if (hp <= 0) return;
        hp--;
        hit_points[hp].enabled = false;
    }

    public int GetHp()
    {
        return hp;
    }
}
