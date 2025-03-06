using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] string next_scene_name = default!;

    public void TitleButton()
    {
        SceneManager.LoadScene(next_scene_name);
    }

}
