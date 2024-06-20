using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pipePrefab;
    [SerializeField]
    private GameObject bonusPrefab;

    private float spawnPeriod = 4f; // кожні 5 секунди
    private float spawnBonusPeriod = 26f; // кожні 13 секунди
    private float timeLeft;
    private float timeBonusLeft;

    void Start()
    {
        timeLeft = 0f; // = spawnPeriod
        timeBonusLeft = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timeBonusLeft -= Time.deltaTime;
        if (timeLeft <= 0f)
        {
            timeLeft = spawnPeriod;
            SpawnPipe();
        }
        if (timeBonusLeft <= 0f)
        {
            timeBonusLeft = spawnBonusPeriod;
            SpawnBonus();
        }
    }

    void SpawnPipe()
    {
        var pipe = GameObject.Instantiate(pipePrefab);
        pipe.transform.position = this.transform.position + Vector3.up * Random.Range(-2f, 2f);
    }

    void SpawnBonus()
    {
        var bonus = GameObject.Instantiate(bonusPrefab);
        bonus.transform.position = this.transform.position + Vector3.up * Random.Range(-4f, 4f);
    }
}
