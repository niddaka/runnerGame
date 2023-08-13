using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    public GameObject coinPrefab;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private Vector3 spawnPosCoin = new Vector3(25, 5, 0);

    [SerializeField]
    private float startDelay;
    [SerializeField]
    private float repeatRate;

    private List<GameObject> spawnObstacles = new List<GameObject>();
    private List<GameObject> spawnCoins = new List<GameObject>();

    private PlayerController playerControllerScript;
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        InvokeRepeating("SpawnCoin", startDelay, repeatRate);
        playerControllerScript = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpawnedObjects();
    }

    void SpawnObstacle()
    {
        if (!playerControllerScript.gameOver)
        {
            int obstacleIndex = Random.Range(0, obstaclePrefab.Length);
            GameObject newObstacle = Instantiate(obstaclePrefab[obstacleIndex], spawnPos, obstaclePrefab[obstacleIndex].transform.rotation);
            spawnObstacles.Add(newObstacle);
        }

    }

    void SpawnCoin()
    {
        if (!playerControllerScript.gameOver)
        {
            GameObject newCoin = Instantiate(coinPrefab, spawnPosCoin, coinPrefab.transform.rotation);
            spawnCoins.Add(newCoin);
        }
    }

    void CheckSpawnedObjects()
    {
        foreach (GameObject obj in spawnObstacles)
        {
            if (obj == null)
            {
                spawnObstacles.Remove(obj);
                break;
            }
        }
        foreach (GameObject obj in spawnCoins)
        {
            if (obj == null)
            {
                spawnCoins.Remove(obj);
                break;
            }
        }
    }
}
