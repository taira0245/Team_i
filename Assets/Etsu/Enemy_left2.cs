using System.Collections;
using UnityEngine;
[System.Obsolete]

public class Enemy_left2 : MonoBehaviour//�����Ă���ړ�
{
    [Header("�����Ă���ړ��X�N���v�g�u���v")]// ��]�Ɋւ���ϐ�
    [Tooltip("�����Ƃ��Ă��g����������")]// ��]�Ɋւ���ϐ�
    [SerializeField] string MEMO;

    Bom bomscript;
    Player countscript;

    bool start = true; // HP
    [Tooltip("���e")]
    [SerializeField] GameObject bom; // ���e�{��
    [Tooltip("�܂�������...")]
    [Header("���i�̒x�����b��")]
    [SerializeField] float start_delay; //�ŏ��̒x��
    [Header("���e���玟�̔��e�܂ŊJ���鎞��")]
    [SerializeField] float delay = 3; //�������㎟������܂ł̒x��

    float time; // 1�b�o�߂��Ƃ�1������
    float speed = 2; // �ړ��X�s�[�h
    bool limit = false; //�ړ��I����true
    bool nextbom = false;

    [Header("��]�֘A")]// ��]�Ɋւ���ϐ�

    [Tooltip("��]���x")]// ��]�Ɋւ���ϐ�
    [SerializeField] float rotationSpeed = 1.5f; // ��]���x
    [Tooltip("��]�̕� (x��)")]
    [SerializeField] float width = 5.0f; // ��]�̕� (x��)
    [Tooltip("��]�̕� (y��)")]
    [SerializeField] float height = 1.5f; // ��]�̍��� (y��)
    [Tooltip("���������b��Ɉړ����J�n���邩")]
    [SerializeField] float warp_delay = 0.15f;


    Vector3 center; // ��]�̒��S

    bool rotating = false; // ��]���Ă��邩�ǂ���
    bool isMovingRight = true; // �E�Ɉړ������ǂ���

    float initialXPosition; // �ŏ��̈ʒu���L�^����

    void Start()
    {
        countscript = GameObject.FindObjectOfType<Player>();
        bomscript = GetComponent<Bom>(); // �X�^�[�g���ɃR���|�[�l���g���擾
        center = new Vector3(Random.Range(5.0f, -5.0f), Random.Range(1f, 2.0f), 0); // �����_���Ȉʒu�ɉ�]�̒��S��ݒ�
    }


    void Update()
    {
        if (isMovingRight)
        {
            Slide(); // �ŏ��̉E�ړ�
        }
        else if (!limit)
        {
            if (start)
            {

                start = false;
                StartCoroutine(Bom_spawnStart()); // ���e�𓊂���

            }
            Delay();
            limit = true;

        }

        // ��]�J�n��ɉ�]���������s
        if (rotating)
        {
            RotateAround();
        }

        // �Փ˔���
        if (bomscript != null && bomscript.change)
        {
            Destroy(gameObject); // Enemy��j��
        }
    }

    void Slide()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime); // �E�Ɉړ�
        time += Time.deltaTime;

        // ��莞�Ԍ�Ɉړ����������A��]���J�n
        if (time > 2)
        {
            isMovingRight = false; // �E�ړ����I�������A��]���J�n

            initialXPosition = transform.position.x; // �ړ��I�����̈ʒu���L�^
        }
    }

    IEnumerator Bom_spawnStart()
    {
        Debug.Log("start");
        // yield return new WaitForSeconds(start_delay);

        Instantiate(bom, transform.position, transform.rotation); // ���e�𓊂���
        yield return new WaitForSeconds(warp_delay);

        rotating = true; // ��������ɉ�]���J�n
        Debug.Log("END");
    }

    IEnumerator Bom_spawn()
    {
        Debug.Log("�X�^�[�g");
        Instantiate(bom, transform.position, transform.rotation); // ���e�𓊂���
        yield return new WaitForSeconds(delay); // ���ɓ�����܂ł̒x��
        Debug.Log("�G���f�B���O1");
        limit = false;
        Debug.Log("�G���f�B���O2");

    }
    void Delay()
    {
        StartCoroutine(Bom_spawn());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("�Փˌ��o: " + other.gameObject.name); // �f�o�b�O���O

        Bom bomObj = other.GetComponent<Bom>();
        if (bomObj != null && bomObj.change)
        {
            Debug.Log("Bom �ƏՓˁIEnemy ��j��"); // �j�󃍃O
            Destroy(gameObject); // �Փ˂����ꍇ��Enemy��j��
            countscript.count++;
        }
    }

    // ��]����
    void RotateAround()
    {
        // ��]�J�n���̈ʒu����ɂ��ĉ�]
        time += Time.deltaTime * rotationSpeed; // ���Ԃɍ��킹�ĉ�]
        float x = center.x + Mathf.Cos(time) * width; // x���W�̌v�Z
        float y = center.y + Mathf.Sin(time) * height; // y���W�̌v�Z

        transform.position = new Vector3(x, y, 0); // �V�����ʒu�Ɉړ�
    }

    // ��]���J�n���鏈��
    public void StartRotating()
    {
        rotating = true;
    }
}
