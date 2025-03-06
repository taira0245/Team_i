using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField] string next_scene_name = default!;

    public void TutorialButton()
    {
        SceneManager.LoadScene(next_scene_name);
    }
}
