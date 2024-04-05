using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnEnemy");
        Debug.Log("Start Spawn");
    }

    // Update is called once per frame
    public float spawnLeastWait;
    public float spawnMostWait;
    private float spawnWait;
    void Update()
    {
        spawnWait = spawnMostWait - spawnLeastWait;
    }

    private int        xPos;
    private int        yPos;
    private int        zPos;
    public int        enemyCount;
    public GameObject enemy;
    IEnumerator SpawnEnemy()
    {
        while (enemyCount<10)
        {
            xPos = Random.Range(-10, 10);
            yPos = Random.Range(-10, 10);
            zPos = Random.Range(-10, 10);
            Instantiate(enemy, this.transform.position + new Vector3(xPos, yPos, zPos),Quaternion.identity);
            yield return new WaitForSeconds(spawnWait);
            enemyCount += 1;
            Debug.Log(enemyCount);
        }
    }
}