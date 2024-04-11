using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float      spawnRadius  = 10f;
    public float      minSpawnTime = 1f;
    public float      maxSpawnTime = 3f;
    public float      EnemyMaxNumber;
    public float      EnemyNumber;


    private float nextSpawnTime;

    void Start()
    {
        // 初始化下一次生成时间
        nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
        EnemyNumber   = 0;
    }

    void Update()
    {
        // 如果当前时间大于等于下一次生成时间
        if (Time.time >= nextSpawnTime&&EnemyNumber<=EnemyMaxNumber)
        {
            // 生成敌人
            SpawnEnemy();
            EnemyNumber++;
            Debug.Log(EnemyNumber);
            // 计算下一次生成时间
            nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
        }
    }

    void SpawnEnemy()
    {
        // 随机生成位置
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;

        // 生成敌人
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}