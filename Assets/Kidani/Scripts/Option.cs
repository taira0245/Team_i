using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    public void OptionButton()
    {
        SceneManager.LoadScene("OptionScene");
    }

}