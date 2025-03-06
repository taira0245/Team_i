using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp = 3;

    public int count = 0;
    bool swing = false;
    public float space_cool_time = 1; //クールタイム

    public GameObject santa1;
    public GameObject santa2;
    public GameObject santa3;


    void Update()
    {
        Swing();
    }



    void Swing()
    {
        if (Input.GetKeyDown(KeyCode.Space) || (Input.GetMouseButtonDown(0)) && swing == false)
        {
            santa1.SetActive(false);
            santa2.SetActive(true);
            santa3.SetActive(false);
            swing = true;
            StartCoroutine(SpaceCooldown());

        }

    }


    IEnumerator SpaceCooldown() // スペースキーのクールタイム
    {
        Debug.Log("クールタイム中...");
        yield return new WaitForSeconds(0.2f);

        santa1.SetActive(false);
        santa2.SetActive(false);
        santa3.SetActive(true);

        yield return new WaitForSeconds(space_cool_time); // f秒待つ
        santa1.SetActive(true);
        santa2.SetActive(false);
        santa3.SetActive(false);
        swing = false; // スペースキーを再び押せるようにする
        Debug.Log("クールタイム終了！スペースキーが再び使えます");
    }

}
