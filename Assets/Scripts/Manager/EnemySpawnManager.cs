using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] Transform cameraPos;


    public GameObject enemyPrefabs;
    public int enemyLimit;
    public float spawnInterval;

    private List<GameObject> enemyContainer = new List<GameObject>();
    private float currentSpawnRate;
    private float lastSpawnRate;


    // Start is called before the first frame update
    void Start()
    {
        currentSpawnRate = 0f;
        lastSpawnRate = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        RemoveNullObject();
        SpawnEnemy();
    }
    void SpawnEnemy()
    {
        if (enemyContainer.Count < enemyLimit)
        {
            currentSpawnRate -= Time.deltaTime;

            if (currentSpawnRate <= 0f)
            {
                enemyContainer.Add(Instantiate(enemyPrefabs, GetRandomCoordinate(), Quaternion.identity));
                currentSpawnRate = lastSpawnRate + spawnInterval;
                lastSpawnRate = currentSpawnRate;
            }
        }
    }

    void RemoveNullObject()
    {
        for (int i = enemyContainer.Count - 1; i >= 0; i--)
        {
            if (enemyContainer[i] == null)
            {
                lastSpawnRate -= spawnInterval;
                currentSpawnRate -= spawnInterval;
                enemyContainer.Remove(enemyContainer[i]);
            }
        }
    }

    Vector3 GetRandomCoordinate()
    {
        float x = Random.Range(cameraPos.transform.position.x - 45, cameraPos.transform.position.x + 46);
        float y = Random.Range(cameraPos.transform.position.y - 25, cameraPos.transform.position.y + 26);

        return new Vector3(x, y, 0f);
    }
}
