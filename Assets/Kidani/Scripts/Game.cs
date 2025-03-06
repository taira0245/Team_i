using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] string next_scene_name = default!;
    public void GameButton()
    {
        SceneManager.LoadScene(next_scene_name);
    }

}
