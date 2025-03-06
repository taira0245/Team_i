using System.Collections;
using UnityEngine;    
[System.Obsolete]

public class Enemy : MonoBehaviour
{

    Bom bomscript;

    bool start = true; // HP
    [SerializeField] GameObject bom; // ���e�{��
    [SerializeField] float start_delay; //�ŏ��̒x��
    [SerializeField] float delay;�@//�������㎟������܂ł̒x��

    float time; // 1�b�o�߂��Ƃ�1������
    float speed = 2; // �ړ��X�s�[�h
    bool limit = false;

    void Start()
    {
        bomscript = GetComponent<Bom>(); // �X�^�[�g���ɃR���|�[�l���g���擾
    }

    void Update()
    {
        if (time <= 2)
        {
            Slide();
        }
        else if (!limit)
        {
            if (start)
            {
                start = false;
                Bom_spawn();

            }
            limit = true;
            Delay();
        }

        // �Փ˔���
        if (bomscript != null && bomscript.change)
        {
            Destroy(gameObject); // Enemy��j��
        }
    }

    void Slide()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        time += Time.deltaTime;
    }

    void Delay()
    {
        StartCoroutine(Bom_spawn());
    }

    IEnumerator Bom_spawn()
    {
        yield return new WaitForSeconds(start_delay);
        Instantiate(bom, transform.position, transform.rotation);
        yield return new WaitForSeconds(delay);
        limit = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("�Փˌ��o: " + other.gameObject.name); // �f�o�b�O���O

        Bom bomObj = other.GetComponent<Bom>();
        if (bomObj != null && bomObj.change)
        {
            Debug.Log("Bom �ƏՓˁIEnemy ��j��"); // �j�󃍃O
            Destroy(gameObject);
        }
    }

}
