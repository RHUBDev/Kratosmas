using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private int fishToSpawn = 3;
    private float fishRange = 30;
    public GameObject fishSpawnPrefab;
    public Transform fishPool;

    private void Start()
    {
        for(int i = 0; i < fishToSpawn; i++)
        {
            AddFish();
        }
    }

    void AddFish()
    {
        Vector3 randomfishpos = new Vector3(Random.Range(-fishRange, fishRange), 5f, 60f + Random.Range(-fishRange, fishRange));
        Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        Instantiate(fishSpawnPrefab, randomfishpos, randomRotation, fishPool);
    }
}
