using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GetKillCount : MonoBehaviour
{
    [Header("“|‚µ‚½“G‚Ì”‚ğ•\¦‚³‚¹‚éTextUI"), SerializeField]
    private Text kill_count_text;

    void Update()
    {
            kill_count_text.text = "Kill : " + ScoreMG.GetLatestScoreData();
        }
}