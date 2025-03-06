using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    public float speed = 2.5f;
    public GameObject destroyEffectPrefab;

    Score score;


    void Start()
    {
        Destroy(gameObject, 60f);

        GameObject scoreObj = GameObject.FindGameObjectWithTag("Score");
        score = scoreObj.GetComponent<Score>();
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (destroyEffectPrefab)
        {
            Destroy(
                Instantiate(destroyEffectPrefab, transform.position, transform.rotation),
                10f
            );
        }

        if (score) 
        {
            score.AddScore();
        }

        Destroy(gameObject);
    }
}
