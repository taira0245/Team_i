using System.Collections;
using UnityEngine;
[System.Obsolete]

public class Enemy_left1 : MonoBehaviour//移動してから投げ
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
    [Tooltip("最低値")]
    [SerializeField] float min_start_delay = 0; //投げた後次投げるまでの遅延
    [Tooltip("最高値")]
    [SerializeField] float max_start_delay = 5; //投げた後次投げるまでの遅延

    float start_delay;

    [Header("爆弾から次の爆弾まで開ける時間")]
    [Tooltip("最低値")]
    [SerializeField] float min_delay = 2; //投げた後次投げるまでの遅延
    [Tooltip("最高値")]
    [SerializeField] float max_delay = 10; //投げた後次投げるまでの遅延

    float delay;

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

    [Header("投げモーション")]// 投げモーション
    [Tooltip("投げた時に出てほしいSprite")]
    [SerializeField] private Sprite throwSprite; // 投げるときのスプライト
    [Tooltip("上のSpriteのサイズ変更")]
    [SerializeField] private Vector3 throwScale = new Vector3(0.4f, 0.4f, 1f); // 投げるときのサイズ
    [Tooltip("何秒後戻したいか")]
    [SerializeField] private float change_sprite = 1f;

    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;
    private Vector3 originalScale;

    void Start()
    {
        countscript = GameObject.FindObjectOfType<Player>();
        bomscript = GetComponent<Bom>(); // スタート時にコンポーネントを取得
        center = new Vector3(Random.Range(5.0f, -5.0f), Random.Range(1f, 2.0f), 0); // ランダムな位置に回転の中心を設定
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRendererを取得
        originalSprite = spriteRenderer.sprite; // 元のスプライトを記憶
        originalScale = transform.localScale; // 元のサイズを記憶
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
        transform.Translate(Vector3.right * speed * Time.deltaTime); // 右に移動
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
            start_delay = Random.Range(min_start_delay, max_start_delay);
            yield return new WaitForSeconds(start_delay);
        }
        ChangeAppearance();

        Instantiate(bom, transform.position, transform.rotation); // 爆弾を投げる
        delay = Random.Range(min_delay, max_delay);
        yield return new WaitForSeconds(delay); // 次に投げるまでの遅延
        limit = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (countscript != null)
        {
            Debug.Log("衝突検出: " + other.gameObject.name); // デバッグログ
            Debug.Log("<color=yellow> 衝突呼ばれた！");

            Bom bomObj = other.GetComponent<Bom>();
            if (bomObj != null && bomObj.change)
            {
                Debug.Log("Bom と衝突！Enemy を破壊"); // 破壊ログ
                Destroy(gameObject); // 衝突した場合にEnemyを破壊
                countscript.count++;
            }
        }
        else
        {
            Debug.Log("countscript == null：Enemy_right.cs");
        }
    }

    // 回転処理
    void RotateAround()
    {
        // 回転開始時の位置を基準にして回転
        time += Time.deltaTime * rotationSpeed; // 時間に合わせて回転
        float x = center.x + Mathf.Cos(time) * width; // x座標の計算
        float y = center.y + Mathf.Sin(time) * height; // y座標の計算

        transform.position = new Vector3(x, y, 0); // 新しい位置に移動
    }

    // 回転を開始する処理
    public void StartRotating()
    {
        rotating = true;
    }

    void ChangeAppearance()
    {
        if (spriteRenderer != null && throwSprite != null)
        {
            spriteRenderer.sprite = throwSprite; // スプライト変更
            transform.localScale = throwScale; // サイズ変更
            StartCoroutine(ResetAppearance()); // 0.4秒後に戻す
        }
    }

    IEnumerator ResetAppearance()
    {
        yield return new WaitForSeconds(change_sprite);
        spriteRenderer.sprite = originalSprite; // スプライトを元に戻す
        transform.localScale = originalScale; // サイズを元に戻す
    }
}
