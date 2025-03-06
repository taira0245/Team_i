using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public GameObject destroyEffectPrefab;


    void Start()
    {
        Destroy(gameObject, 60f);
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

        Destroy(gameObject);
    }
}
