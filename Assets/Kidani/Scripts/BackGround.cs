using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChangeOnTap : MonoBehaviour
{
    [SerializeField] string next_scene_name = default!;
    void Update()
    {
        // ��ʂ��^�b�v������V�[����ύX
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(next_scene_name); // �J�ڐ�̃V�[�������w��
        }
    }
}