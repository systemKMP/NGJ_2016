using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

[Serializable]
public class EnemyDefinition
{
    public EnemyBase Enemy;
    public float Value;
}

public class EnemySpawner : MonoBehaviour {

    public List<EnemyDefinition> Enemies;

    public GameObject SpawnEffect;

    public float MinInterval;
    public float MaxInterval;
    private float _currentTime;

    public float StartingPointGainSpeed;
    public float PointGainClimb;
    public float PointGainSpeed;

    private float _currentPoints;

    public float MinSpawnDist;
    public float MaxSpawnDist;

    public float MinClusterSize;
    public float MaxClusterSize;

    public float InClusterSpawnInterval;

    void Start()
    {
        PointGainSpeed = StartingPointGainSpeed;
        _currentTime = MaxInterval;
    }

    void Update()
    {
        _currentPoints += PointGainSpeed * Time.deltaTime;
        PointGainSpeed += PointGainClimb * Time.deltaTime;

        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0.0f)
        {
            var enem = SelectEnemyType(_currentPoints);
            int enemyCount = (int)(_currentPoints / enem.Value);

            _currentPoints -= enemyCount * enem.Value;

            StartCoroutine(SpawnEnemies(enemyCount, enem));
            _currentTime += Random.Range(MinInterval, MaxInterval);
        }


    }

    private IEnumerator SpawnEnemies(int count, EnemyDefinition EnemyDef)
    {

        var positions = CreateSpawnPositions(count);

        for (int i = 0; i < count; i++)
        {
            Instantiate(EnemyDef.Enemy, positions[i], Quaternion.identity);
            yield return new WaitForSeconds(InClusterSpawnInterval);
            Instantiate(SpawnEffect, positions[i], Quaternion.identity);
        }
    }


    private EnemyDefinition SelectEnemyType(float givenValue)
    {
        var viableEnemies = new List<EnemyDefinition>();

        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].Value < givenValue)
            {
                viableEnemies.Add(Enemies[i]);
            }
        }
        return viableEnemies[Random.Range(0, viableEnemies.Count)];
    }


    public List<Vector3> CreateSpawnPositions(int count)
    {
        Vector3 fromPoint = Player.Instance.transform.position;

        Vector3 direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;

        List<Vector3> positions = new List<Vector3>();

        Vector3 SpawnCenter = direction * Random.Range(MinSpawnDist, MaxSpawnDist) + fromPoint;

        float ballSize = Random.Range(MinClusterSize, MaxClusterSize);

        for (int i = 0; i < count; i++)
        {
            direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;

            positions.Add(SpawnCenter + direction * Random.Range(0.0f, ballSize));
        }

        return positions;
    }
}
