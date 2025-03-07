using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("ステータス")]
    [Tooltip("体力")]
    [SerializeField] public int hp = 3;
    [Tooltip("撃墜数")]
    [SerializeField] public int count = 0;
    
    bool swing = false;
    [Tooltip("クールタイム時間")]
    public float space_cool_time = 1; // クールタイム

    [Header("バッティング")]
    public GameObject santa1;
    public GameObject santa2;
    public GameObject santa3;

    [Header("背景")]
    [Tooltip("最後まで残ります")]
    [SerializeField] public GameObject purezent1;
    [Tooltip("体力2まで表示します")]
    [SerializeField] public GameObject purezent2;
    [Tooltip("体力最大時(3)のみ表示します")]
    [SerializeField] public GameObject purezent3;


    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !swing)
        {
            StartCoroutine(Swing());
        }
        
        if(hp == 3)
        {
            purezent1.SetActive(true);
            purezent2.SetActive(true);
            purezent3.SetActive(true);
        }
        if (hp == 2)
        {
            purezent3.SetActive(false);
        }
        if (hp == 1)
        {
            purezent2.SetActive(false);

        }
        if (hp == 0)
        {
            purezent1.SetActive(false);
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
