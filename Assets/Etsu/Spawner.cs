using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float interval = 10f; //何秒ごとに出現
    public int vertical_range = 3; // 縦軸のランダム範囲
    bool start = true;//初回のみ

    float timer;

    void Update()
    {
        if (start)
        {
            float randomY = Random.Range(-vertical_range, vertical_range);
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + randomY, transform.position.z);
            GameObject obj = Instantiate(prefab, transform.position, transform.rotation);
            start = false;
        }

        timer += Time.deltaTime;

        if (timer > interval)
        {
            timer = 0;

            // ランダムなY座標を生成
            float randomY = Random.Range(-vertical_range, vertical_range);
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + randomY, transform.position.z);

            GameObject obj = Instantiate(prefab, spawnPosition, transform.rotation);
        }
    }
}