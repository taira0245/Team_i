using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score;
    Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    public void AddScore()
    {
        score += 100;
        text.text = "Score : " + score.ToString();
    }
}
