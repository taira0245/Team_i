using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Advanced : MonoBehaviour
{
    [SerializeField] string next_scene_name = default!;
    [SerializeField] ScreenFade screenfade;
    [SerializeField, Range(0, 2)] float fadetime;
    public void AdvancedButton()
    {

        AudioMG.PlaySE("Click");
        //SceneManager.LoadScene(next_scene_name);
        screenfade.SetSceneChange(next_scene_name, fadetime);
    }

}
