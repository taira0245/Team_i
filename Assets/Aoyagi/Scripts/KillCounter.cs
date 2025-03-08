using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCounter : MonoBehaviour
{
    [Header("倒した敵の数"), SerializeField] 
    private int kill_count;
    [Header("倒した敵の数を表示させるTextUI"), SerializeField]
    private Text kill_count_text;

    //キルスコアのリセット
    private void Start()
    {
        kill_count = 0;
        kill_count_text.text = "" + kill_count;
    }

    //この関数を一回実行するとキルスコアが1増える
    public void KillCountPlus()
    {
        kill_count++;
        kill_count_text.text = "" + kill_count;
    }

    //キルカウント取得用
    public int GetKillCount()
    {
        return kill_count;
    }
}
