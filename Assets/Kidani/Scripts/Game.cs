using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    [SerializeField] string next_scene_name = default!;
    public void StartButton()
    {
        SceneManager.LoadScene(next_scene_name);
    }

}
