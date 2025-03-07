using System.Collections;
using UnityEngine;
[System.Obsolete]

public class Enemy_right : MonoBehaviour
{
    Bom bomscript;
    Player countscript;

    bool start = true; // HP
    [SerializeField] GameObject bom; // ���e�{��
    [SerializeField] float start_delay; //�ŏ��̒x��
    [SerializeField] float delay; //�������㎟������܂ł̒x��

    float time; // 1�b�o�߂��Ƃ�1������
    float speed = 2; // �ړ��X�s�[�h
    bool limit = false; //�ړ��I����true

    // ��]�Ɋւ���ϐ�
    float rotationSpeed = 1.5f; // ��]���x
    float width = 5.0f; // ��]�̕� (x��)
    float height = 1.5f; // ��]�̍��� (y��)
    Vector3 center; // ��]�̒��S

    bool rotating = false; // ��]���Ă��邩�ǂ���
    bool isMovingRight = true; // �E�Ɉړ������ǂ���

    float initialXPosition; // �ŏ��̈ʒu���L�^����

    void Start()
    {
        countscript = GameObject.FindObjectOfType<Player>();
        bomscript = GetComponent<Bom>(); // �X�^�[�g���ɃR���|�[�l���g���擾
        center = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(0.0f, 5.0f), 0); // �����_���Ȉʒu�ɉ�]�̒��S��ݒ�
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
                Bom_spawn(); // ���e�𓊂���
            }
            limit = true;
            Delay();
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
        transform.Translate(Vector3.left * speed * Time.deltaTime); // �E�Ɉړ�
        time += Time.deltaTime;

        // ��莞�Ԍ�Ɉړ����������A��]���J�n
        if (time > 2)
        {
            isMovingRight = false; // �E�ړ����I�������A��]���J�n
            rotating = true; // ��]���J�n
            initialXPosition = transform.position.x; // �ړ��I�����̈ʒu���L�^
        }
    }

    void Delay()
    {
        StartCoroutine(Bom_spawn());
    }

    IEnumerator Bom_spawn()
    {
        yield return new WaitForSeconds(start_delay);
        Instantiate(bom, transform.position, transform.rotation); // ���e�𓊂���
        yield return new WaitForSeconds(delay); // ���ɓ�����܂ł̒x��
        limit = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("�Փˌ��o: " + other.gameObject.name); // �f�o�b�O���O
        Debug.Log("<color=yellow> �ՓˌĂ΂ꂽ�I");

        Bom bomObj = other.GetComponent<Bom>();
        if (countscript != null) {
            if (bomObj != null && bomObj.change) {
                Debug.Log("Bom �ƏՓˁIEnemy ��j��"); // �j�󃍃O
                Destroy(gameObject); // �Փ˂����ꍇ��Enemy��j��
                countscript.count++;
            }
        }
        else {
            Debug.Log("countscript == null�FEnemy_right.cs");
        }
    }

    // ��]����
    void RotateAround()
    {
        // ��]�J�n���̈ʒu����ɂ��ĉ�]
        time += Time.deltaTime * rotationSpeed; // ���Ԃɍ��킹�ĉ�]
        float x = center.x + Mathf.Sin(time) * width; // x���W�̌v�Z
        float y = center.y + Mathf.Cos(time) * height; // y���W�̌v�Z

        transform.position = new Vector3(x, y, 0); // �V�����ʒu�Ɉړ�
    }

    // ��]���J�n���鏈��
    public void StartRotating()
    {
        rotating = true;
    }
}
