using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp = 3;

    public int count = 0;
    bool swing = false;
    public float space_cool_time = 1;

    public List<GameObject> santa;


    void Update()
    {
        Swing();
    }



    void Swing()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !swing)
        {
            Debug.Log("スペースキーが押された！クールタイム開始");
            foreach (GameObject obj in santa)
            {
                if (obj != null)
                {
                    // 現在の状態に応じてアクティブ/非アクティブを切り替える
                    obj.SetActive(!obj.activeSelf);
                }
            }
            StartCoroutine(SpaceCooldown()); // クールタイム開始

        }
    }


    IEnumerator SpaceCooldown() // スペースキーのクールタイム
    {
        Debug.Log("クールタイム中...");
        yield return new WaitForSeconds(space_cool_time); // f秒待つ
        foreach (GameObject obj in santa)
        {
            if (obj != null)
            {
                // 現在の状態に応じてアクティブ/非アクティブを切り替える
                obj.SetActive(!obj.activeSelf);
            }
        }
        StartCoroutine(SpaceCooldown()); // クールタイム開始
        swing = false; // スペースキーを再び押せるようにする
        Debug.Log("クールタイム終了！スペースキーが再び使えます");
    }

}
