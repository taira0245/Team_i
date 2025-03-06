using System.Collections;
using UnityEngine;    
[System.Obsolete]

public class Enemy : MonoBehaviour
{

    Bom bomscript;

    bool start = true; // HP
    [SerializeField] GameObject bom; // 爆弾本体
    [SerializeField] float start_delay; //最初の遅延
    [SerializeField] float delay;　//投げた後次投げるまでの遅延

    float time; // 1秒経過ごとに1増える
    float speed = 2; // 移動スピード
    bool limit = false;

    void Start()
    {
        bomscript = GetComponent<Bom>(); // スタート時にコンポーネントを取得
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

        // 衝突判定
        if (bomscript != null && bomscript.change)
        {
            Destroy(gameObject); // Enemyを破壊
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
        Debug.Log("衝突検出: " + other.gameObject.name); // デバッグログ

        Bom bomObj = other.GetComponent<Bom>();
        if (bomObj != null && bomObj.change)
        {
            Debug.Log("Bom と衝突！Enemy を破壊"); // 破壊ログ
            Destroy(gameObject);
        }
    }

}
