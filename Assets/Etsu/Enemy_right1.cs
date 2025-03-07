using System.Collections;
using UnityEngine;
[System.Obsolete]

public class Enemy_right1 : MonoBehaviour//移動してから投げ
{
    [Header("移動してから投げスクリプト「右」")]// 回転に関する変数
    [Tooltip("メモとしてお使いください")]// 回転に関する変数
    [SerializeField] string MEMO;

    Bom bomscript;
    Player countscript;

    bool start = true; // HP
    [Tooltip("爆弾")]
    [SerializeField] GameObject bom; // 爆弾本体
    [Header("初段の遅延何秒か")]
    [SerializeField] float start_delay; //最初の遅延
    [Header("爆弾から次の爆弾まで開ける時間")]
    [SerializeField] float delay = 3; //投げた後次投げるまでの遅延

    float time; // 1秒経過ごとに1増える
    float speed = 2; // 移動スピード
    bool limit = false; //移動終わればtrue

    [Header("回転関連")]// 回転に関する変数

    [Tooltip("回転速度")]
    [SerializeField] float rotationSpeed = 1.5f; // 回転速度
    [Tooltip("回転の幅 (x軸)")]
    [SerializeField] float width = 5.0f; // 回転の幅 (x軸)
    [Tooltip("回転の幅 (y軸)")]
    [SerializeField] float height = 1.5f; // 回転の高さ (y軸)
    Vector3 center; // 回転の中心

    bool rotating = false; // 回転しているかどうか
    bool isMovingRight = true; // 右に移動中かどうか

    float initialXPosition; // 最初の位置を記録する

    void Start()
    {
        countscript = GameObject.FindObjectOfType<Player>();
        bomscript = GetComponent<Bom>(); // スタート時にコンポーネントを取得
        center = new Vector3(Random.Range(5.0f, -5.0f), Random.Range(1f, 2.0f), 0); // ランダムな位置に回転の中心を設定
    }


    void Update()
    {
        if (isMovingRight)
        {
            Slide(); // 最初の右移動
        }
        else if (!limit)
        {
            if (start)
            {
                rotating = true; // 回転を開始
                Bom_spawn(); // 爆弾を投げる
            }
            limit = true;
            Delay();
        }

        // 回転開始後に回転処理を実行
        if (rotating)
        {
            RotateAround();
        }

        // 衝突判定
        if (bomscript != null && bomscript.change)
        {
            Destroy(gameObject); // Enemyを破壊
        }
    }

    void Slide()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime); // 右に移動
        time += Time.deltaTime;

        // 一定時間後に移動が完了し、回転を開始
        if (time > 2)
        {
            isMovingRight = false; // 右移動が終わったら、回転を開始
            rotating = true; // 回転を開始
            initialXPosition = transform.position.x; // 移動終了時の位置を記録
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
        Instantiate(bom, transform.position, transform.rotation); // 爆弾を投げる
        yield return new WaitForSeconds(delay); // 次に投げるまでの遅延
        limit = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("衝突検出: " + other.gameObject.name); // デバッグログ

        Bom bomObj = other.GetComponent<Bom>();
        if (bomObj != null && bomObj.change)
        {
            Debug.Log("Bom と衝突！Enemy を破壊"); // 破壊ログ
            Destroy(gameObject); // 衝突した場合にEnemyを破壊
            countscript.count++;
        }
    }

    // 回転処理
    void RotateAround()
    {
        // 回転開始時の位置を基準にして回転
        time += Time.deltaTime * rotationSpeed; // 時間に合わせて回転
        float x = center.x + Mathf.Sin(time) * width; // x座標の計算
        float y = center.y + Mathf.Cos(time) * height; // y座標の計算

        transform.position = new Vector3(x, y, 0); // 新しい位置に移動
    }

    // 回転を開始する処理
    public void StartRotating()
    {
        rotating = true;
    }
}
