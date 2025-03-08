using System.Collections;
using UnityEngine;    
[System.Obsolete]

public class Bom : MonoBehaviour
{
    [SerializeField] float throw_speed = 1f; // ストライクゾーンに向かうスピード
    [SerializeField] float player_speed = 3f; // マウスの位置に向かうスピード

    bool strike_bool = false; // トリガー感知
   public bool change = false; // 切り替え
    Vector3 move_direction; // 移動方向
    Transform strike_zone; // ストライクゾーン

    bool space_cool_time = true; // スペースキーのクールタイム管理

    Player hpscript;


    void Start()
    {
        hpscript = GameObject.FindObjectOfType<Player>();
        Strike_zone(); // ストライクゾーンの検知
        if (strike_zone != null)
        {
            move_direction = (strike_zone.position - transform.position).normalized;
        }
    }

    void Update()
    {
        if (!change)
        {
            Bomb_throwing(); // ストライクゾーンへ移動
        }
        else
        {
            bomb_reflex(); // マウスの位置へ移動
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && strike_bool && space_cool_time) // スペースキーのクールタイム判定
        {
            

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // 2DなのでZ軸を固定
            move_direction = (mousePosition - transform.position).normalized;
            change = true; // 方向変更フラグ

            AudioMG.PlaySE("Hit");
            StartCoroutine(SpaceCooldown()); // クールタイム開始
            Destroy(gameObject, 4f); // 4秒後にオブジェクトを削除
        }

       else if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && space_cool_time)
        {
            AudioMG.PlaySE("Swing");
            Debug.Log("スペースキーが押された！クールタイム開始");
                StartCoroutine(SpaceCooldown()); // クールタイム開始
        
        }

    }

    void Strike_zone() // ストライクゾーンの検知
    {
        GameObject strikeObj = GameObject.FindWithTag("strike");
        if (strikeObj != null)
        {
            strike_zone = strikeObj.transform;
        }
    }

    void Bomb_throwing() // ストライクゾーンへ移動
    {
        transform.Translate(move_direction * throw_speed * Time.deltaTime, Space.World);
    }

    void bomb_reflex() // マウスの位置へ移動
    {
        transform.Translate(move_direction * player_speed * Time.deltaTime, Space.World);
    }

    void OnTriggerStay2D(Collider2D other) // ストライクゾーンのトリガー感知
    {
        if (other.CompareTag("strike"))
        {
            strike_bool = true;
            Debug.Log("ストライクゾーン内にいる！");
        }
    }

    void OnTriggerExit2D(Collider2D other) // トリガーから出たとき
    {
        if (other.CompareTag("strike"))
        {
            if (!change) // スペースを押していなかった場合のみ削除
            {
                Debug.Log("ストライクゾーンから出た！爆弾を削除します");
                hpscript.hp--;
                Destroy(gameObject);
                AudioMG.PlaySE("Miss");
            }
            else
            {
                Debug.Log("ストライクゾーンから出たが、スペースが押されているため削除しない");
            }
            strike_bool = false;
        }
    }

    IEnumerator SpaceCooldown() // スペースキーのクールタイム
    {
        space_cool_time = false; // すぐに無効化
        Debug.Log("クールタイム中...");
        yield return new WaitForSeconds(hpscript.space_cool_time + 0.1f); // f秒待つ
        space_cool_time = true; // スペースキーを再び押せるようにする
        Debug.Log("クールタイム終了！スペースキーが再び使えます");
    }
}
