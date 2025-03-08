using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChangeOnTap : MonoBehaviour
{
    [SerializeField] string next_scene_name = default!;
    [SerializeField] ScreenFade screenfade;
    [SerializeField, Range(0, 2)] float fadetime;
    void Update()
    {
        // ��ʂ��^�b�v������V�[����ύX
        if (Input.GetMouseButtonDown(0))
        {
            screenfade.SetSceneChange(next_scene_name, fadetime);
        }
    }
}