using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class ButterPoint : MonoBehaviour
{
    [Header("ノーツ"), SerializeField] 
    private Image notes_ring;
    [Header("ノーツの大きさ"), SerializeField]
    private float scale, max_scale;
    [Header("ノーツが小さくなるスピード"), SerializeField]
    private float notes_speed;
    [Header("固定リングのアニメーション"),SerializeField]
    private Animator animator;

    private void Start()
    {
        scale = max_scale;
    }

    public void Update()
    {
        NotesReduction();

        if (Input.GetButtonDown("Fire1"))
        {
            if(scale >= 0.9 && scale <= 1.0f)
            {
                animator.SetTrigger("success");
                NotesReset();
            }
        }
    }

    //ノーツを徐々に小さくする
    public void NotesReduction()
    {
        scale -= notes_speed * Time.deltaTime;
        notes_ring.transform.localScale = new Vector3(scale, scale, scale);
        if (scale <= 0)
        {
            NotesReset();
        }
    }

    //ノーツを元の位置に戻す
    public void NotesReset()
    {
        scale = max_scale;
    }
}
