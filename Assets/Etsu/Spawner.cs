using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("召喚したいオブジェクト")]
    public GameObject prefab;
    [Header("出現させたい最低値")]
    public float min_interval = 13f; //何秒ごとに出現
    [Header("出現させたい最低値")]
    public float max_interval = 7f; //何秒ごとに出現
    [Header("出現させたい範囲[縦]")]
    public float vertical_range = 3; // 縦軸のランダム範囲

    float interval;
    bool start = true;//初回のみ

    float timer;

    void Update()
    {
        if (start)
        {
            float randomY = Random.Range(-vertical_range, vertical_range);
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + randomY, transform.position.z);
            GameObject obj = Instantiate(prefab, transform.position, transform.rotation);
            interval = Random.Range(min_interval, max_interval);
            start = false;
        }

        timer += Time.deltaTime;

        if (timer > interval)
        {
            timer = 0;
            interval = Random.Range(min_interval, max_interval);

            // ランダムなY座標を生成
            float randomY = Random.Range(-vertical_range, vertical_range);
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + randomY, transform.position.z);

            GameObject obj = Instantiate(prefab, spawnPosition, transform.rotation);
        }
    }
}