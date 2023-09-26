using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject meteorPrefab;
    public float spawnRatePerMinute = 30f;
    public float spawnRateIncrement = 1f;
    private float spawnNext = 0;
    public float xLimit;
    public float maxTimeLife = 4f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnNext)
        {
            spawnNext = Time.time + 60 / spawnRatePerMinute;

            spawnRatePerMinute += spawnRateIncrement;

            float rand = Random.Range(-xLimit, xLimit);

            Vector2 spawnPosition = new Vector2(rand, 8f);
            
            GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);

            Destroy(meteor, maxTimeLife);
        }
    }
}
