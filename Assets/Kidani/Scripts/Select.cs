using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Select : MonoBehaviour
{
    [SerializeField] string next_scene_name = default!;

    public void SelectButton()
    {
        SceneManager.LoadScene(next_scene_name);
    }

}
