using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public CharacterPlayer player;
    public Color red, blue;
    public Projectile projectilePrefab;
    public CharacterEnemy enemyPrefab;
    public List<Transform> enemySpawnPoints;

    [SerializeField] 
    public int score;
    
    [SerializeField]
    private List<Transform> availableSpawnPoints = new List<Transform>();
    private bool canSpawn = true;
    
    public Vector2 enemySpawnSecondsRange;
    public int spawnDecrementFactor = 4;
    private float spawnDecrementTime = 0.25f;

    [Space(20)]
    public Vector2 enemyMovementTimeRange;
    public float movementDecrementTime = 0.3f;
    
    [SerializeField]
    private float currentMovementTime;
    
    [SerializeField]
    private float enemySpawnTime, currentTime;
    private int spawnedEnemies;
    
    public event Action onGameOverEvent;
    public static GameManager instance; 
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    private void Start()
    {
        RefillSpawnPointList();
        enemySpawnTime = enemySpawnSecondsRange.y;
        currentMovementTime = enemyMovementTimeRange.y;
    }
    
    private void Update()
    {
        if (!canSpawn)
        {
            return;
        }
        
        currentTime += Time.deltaTime;
        if (currentTime >= enemySpawnTime)
        {
            SpawnEnemy();
            currentTime = 0;
        }
    }

    private void RefillSpawnPointList()
    {
        for (int i = 0; i < enemySpawnPoints.Count; i++)
        {
            availableSpawnPoints.Add(enemySpawnPoints[i]);
        }

    }

    private void DecrementSpawnRate()
    {
        if (spawnedEnemies % spawnDecrementFactor == 0)
        {
            enemySpawnTime -= spawnDecrementTime;
            enemySpawnTime = Mathf.Clamp(enemySpawnTime, enemySpawnSecondsRange.x, enemySpawnSecondsRange.y);

            currentMovementTime -= movementDecrementTime;
            currentMovementTime = Mathf.Clamp(currentMovementTime, enemyMovementTimeRange.x, enemyMovementTimeRange.y);
        }
    }

    public Color GetColor(EColorType colorType)
    {
        switch (colorType)
        {
            case EColorType.Red:
                return red;
            
            case EColorType.Blue:
                return blue;
        }
        
        return Color.white;
    }

    public void SpawnEnemy()
    {
        if (availableSpawnPoints.Count <= 0)
        {
            RefillSpawnPointList();
        }
        
        int randomIndex = Random.Range(0, availableSpawnPoints.Count);
        Transform spawnPoint = availableSpawnPoints[randomIndex];
        availableSpawnPoints.Remove(spawnPoint);

        CharacterEnemy enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        enemy.Move(currentMovementTime);
        spawnedEnemies++;
        DecrementSpawnRate();
    }

    public void EndGame()
    {
        canSpawn = false;
        onGameOverEvent?.Invoke();
    }
}
