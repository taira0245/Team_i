using System.Collections;
using UnityEngine;
[System.Obsolete]

public class Enemy_left1 : MonoBehaviour//�ړ����Ă��瓊��
{
    [Header("�ړ����Ă��瓊���X�N���v�g�u�E�v")]// ��]�Ɋւ���ϐ�
    [Tooltip("�����Ƃ��Ă��g����������")]// ��]�Ɋւ���ϐ�
    [SerializeField] string MEMO;

    Bom bomscript;
    Player countscript;

    bool start = true; // HP
    [Tooltip("���e")]
    [SerializeField] GameObject bom; // ���e�{��
    [Header("���i�̒x�����b��")]
    [SerializeField] float start_delay; //�ŏ��̒x��
    [Header("���e���玟�̔��e�܂ŊJ���鎞��")]
    [SerializeField] float delay = 3; //�������㎟������܂ł̒x��

    float time; // 1�b�o�߂��Ƃ�1������
    float speed = 2; // �ړ��X�s�[�h
    bool limit = false; //�ړ��I����true

    [Header("��]�֘A")]// ��]�Ɋւ���ϐ�

    [Tooltip("��]���x")]
    [SerializeField] float rotationSpeed = 1.5f; // ��]���x
    [Tooltip("��]�̕� (x��)")]
    [SerializeField] float width = 5.0f; // ��]�̕� (x��)
    [Tooltip("��]�̕� (y��)")]
    [SerializeField] float height = 1.5f; // ��]�̍��� (y��)
    Vector3 center; // ��]�̒��S

    bool rotating = false; // ��]���Ă��邩�ǂ���
    bool isMovingRight = true; // �E�Ɉړ������ǂ���

    float initialXPosition; // �ŏ��̈ʒu���L�^����

    [Header("�������[�V����")]// �������[�V����
    [Tooltip("���������ɏo�Ăق���Sprite")]
    [SerializeField] private Sprite throwSprite; // ������Ƃ��̃X�v���C�g
    [Tooltip("���Sprite�̃T�C�Y�ύX")]
    [SerializeField] private Vector3 throwScale = new Vector3(0.4f, 0.4f, 1f); // ������Ƃ��̃T�C�Y
    [Tooltip("���b��߂�������")]
    [SerializeField] private float change_sprite = 1f;

    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;
    private Vector3 originalScale;

    void Start()
    {
        countscript = GameObject.FindObjectOfType<Player>();
        bomscript = GetComponent<Bom>(); // �X�^�[�g���ɃR���|�[�l���g���擾
        center = new Vector3(Random.Range(5.0f, -5.0f), Random.Range(1f, 2.0f), 0); // �����_���Ȉʒu�ɉ�]�̒��S��ݒ�
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer���擾
        originalSprite = spriteRenderer.sprite; // ���̃X�v���C�g���L��
        originalScale = transform.localScale; // ���̃T�C�Y���L��
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
                rotating = true; // ��]���J�n
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
        transform.Translate(Vector3.right * speed * Time.deltaTime); // �E�Ɉړ�
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
        if (start)
        {
            start = false;
            yield return new WaitForSeconds(start_delay);
        }
        ChangeAppearance();

        Instantiate(bom, transform.position, transform.rotation); // ���e�𓊂���
        AudioMG.PlaySE("Throw");
        yield return new WaitForSeconds(delay); // ���ɓ�����܂ł̒x��
        limit = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (countscript != null)
        {
            Debug.Log("�Փˌ��o: " + other.gameObject.name); // �f�o�b�O���O
            Debug.Log("<color=yellow> �ՓˌĂ΂ꂽ�I");

            Bom bomObj = other.GetComponent<Bom>();
            if (bomObj != null && bomObj.change)
            {
                Debug.Log("Bom �ƏՓˁIEnemy ��j��"); // �j�󃍃O
                Destroy(gameObject); // �Փ˂����ꍇ��Enemy��j��
                countscript.count++;
                AudioMG.PlaySE("Kill");
            }
        }
        else
        {
            Debug.Log("countscript == null�FEnemy_right.cs");
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

    void ChangeAppearance()
    {
        if (spriteRenderer != null && throwSprite != null)
        {
            spriteRenderer.sprite = throwSprite; // �X�v���C�g�ύX
            transform.localScale = throwScale; // �T�C�Y�ύX
            StartCoroutine(ResetAppearance()); // 0.4�b��ɖ߂�
        }
    }

    IEnumerator ResetAppearance()
    {
        yield return new WaitForSeconds(change_sprite);
        spriteRenderer.sprite = originalSprite; // �X�v���C�g�����ɖ߂�
        transform.localScale = originalScale; // �T�C�Y�����ɖ߂�
    }


}
