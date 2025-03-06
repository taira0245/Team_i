using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    [SerializeField] string next_scene_name = default!;
    public void OptionButton()
    {
        SceneManager.LoadScene(next_scene_name);
    }

}