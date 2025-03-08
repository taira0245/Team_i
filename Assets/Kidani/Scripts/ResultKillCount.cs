using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GetKillCount : MonoBehaviour
{
    [Header("倒した敵の数を表示させるTextUI"), SerializeField]
    private Text kill_count_text;

    void Update()
    {
        kill_count_text.text = "Kill : " + ScoreMG.GetLatestScoreData();
    }
}