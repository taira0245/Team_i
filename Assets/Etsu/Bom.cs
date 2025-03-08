using System.Collections;
using UnityEngine;    
[System.Obsolete]

public class Bom : MonoBehaviour
{
    [SerializeField] float throw_speed = 1f; // �X�g���C�N�]�[���Ɍ������X�s�[�h
    [SerializeField] float player_speed = 3f; // �}�E�X�̈ʒu�Ɍ������X�s�[�h

    bool strike_bool = false; // �g���K�[���m
   public bool change = false; // �؂�ւ�
    Vector3 move_direction; // �ړ�����
    Transform strike_zone; // �X�g���C�N�]�[��

    bool space_cool_time = true; // �X�y�[�X�L�[�̃N�[���^�C���Ǘ�

    Player hpscript;


    void Start()
    {
        hpscript = GameObject.FindObjectOfType<Player>();
        Strike_zone(); // �X�g���C�N�]�[���̌��m
        if (strike_zone != null)
        {
            move_direction = (strike_zone.position - transform.position).normalized;
        }
    }

    void Update()
    {
        if (!change)
        {
            Bomb_throwing(); // �X�g���C�N�]�[���ֈړ�
        }
        else
        {
            bomb_reflex(); // �}�E�X�̈ʒu�ֈړ�
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && strike_bool && space_cool_time) // �X�y�[�X�L�[�̃N�[���^�C������
        {
            

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // 2D�Ȃ̂�Z�����Œ�
            move_direction = (mousePosition - transform.position).normalized;
            change = true; // �����ύX�t���O

            AudioMG.PlaySE("Hit");
            StartCoroutine(SpaceCooldown()); // �N�[���^�C���J�n
            Destroy(gameObject, 4f); // 4�b��ɃI�u�W�F�N�g���폜
        }

       else if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && space_cool_time)
        {
            AudioMG.PlaySE("Swing");
            Debug.Log("�X�y�[�X�L�[�������ꂽ�I�N�[���^�C���J�n");
                StartCoroutine(SpaceCooldown()); // �N�[���^�C���J�n
        
        }

    }

    void Strike_zone() // �X�g���C�N�]�[���̌��m
    {
        GameObject strikeObj = GameObject.FindWithTag("strike");
        if (strikeObj != null)
        {
            strike_zone = strikeObj.transform;
        }
    }

    void Bomb_throwing() // �X�g���C�N�]�[���ֈړ�
    {
        transform.Translate(move_direction * throw_speed * Time.deltaTime, Space.World);
    }

    void bomb_reflex() // �}�E�X�̈ʒu�ֈړ�
    {
        transform.Translate(move_direction * player_speed * Time.deltaTime, Space.World);
    }

    void OnTriggerStay2D(Collider2D other) // �X�g���C�N�]�[���̃g���K�[���m
    {
        if (other.CompareTag("strike"))
        {
            strike_bool = true;
            Debug.Log("�X�g���C�N�]�[�����ɂ���I");
        }
    }

    void OnTriggerExit2D(Collider2D other) // �g���K�[����o���Ƃ�
    {
        if (other.CompareTag("strike"))
        {
            if (!change) // �X�y�[�X�������Ă��Ȃ������ꍇ�̂ݍ폜
            {
                Debug.Log("�X�g���C�N�]�[������o���I���e���폜���܂�");
                hpscript.hp--;
                Destroy(gameObject);
                AudioMG.PlaySE("Miss");
            }
            else
            {
                Debug.Log("�X�g���C�N�]�[������o�����A�X�y�[�X��������Ă��邽�ߍ폜���Ȃ�");
            }
            strike_bool = false;
        }
    }

    IEnumerator SpaceCooldown() // �X�y�[�X�L�[�̃N�[���^�C��
    {
        space_cool_time = false; // �����ɖ�����
        Debug.Log("�N�[���^�C����...");
        yield return new WaitForSeconds(hpscript.space_cool_time + 0.1f); // f�b�҂�
        space_cool_time = true; // �X�y�[�X�L�[���Ăщ�����悤�ɂ���
        Debug.Log("�N�[���^�C���I���I�X�y�[�X�L�[���Ăюg���܂�");
    }
}
