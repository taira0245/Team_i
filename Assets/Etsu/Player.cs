using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp = 3;
    public int count = 0;
    bool swing = false;
    public float space_cool_time = 1; // クールタイム

    public GameObject santa1;
    public GameObject santa2;
    public GameObject santa3;

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !swing)
        {
            StartCoroutine(Swing());
        }
    }

    IEnumerator Swing()
    {
        swing = true; // スイング開始
        Debug.Log("攻撃開始");

        santa1.SetActive(false);
        santa2.SetActive(true);
        santa3.SetActive(false);

        Debug.Log("クールタイム中...");
        yield return new WaitForSeconds(0.1f);

        santa1.SetActive(false);
        santa2.SetActive(false);
        santa3.SetActive(true);

        yield return new WaitForSeconds(space_cool_time);

        santa1.SetActive(true);
        santa2.SetActive(false);
        santa3.SetActive(false);
        swing = false; // クールタイム終了後、再度攻撃可能に

        Debug.Log("クールタイム終了！スペースキーが再び使えます");
    }
}
