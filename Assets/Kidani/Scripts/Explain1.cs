using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Explain1 : MonoBehaviour
{
    [SerializeField] string next_scene_name = default!;
    [SerializeField] ScreenFade screenfade;
    [SerializeField, Range(0, 2)] float fadetime;
    public void Explain1Button()
    {
        //SceneManager.LoadScene(next_scene_name);
        screenfade.SetSceneChange(next_scene_name, fadetime);
    }

}
