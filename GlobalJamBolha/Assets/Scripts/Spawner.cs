using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] m_Prefabs;
    [SerializeField] private Transform spawnXPosition;
    [SerializeField] private float spawnIntervalMin = 1;
    [SerializeField] private float spawnIntervalMax = 4;
    
    [SerializeField] private int allTimeSpawnAmountMin = 1;
    [SerializeField] private int allTimeSpawnAmountMax = 7;
    private int currentSpawnAmountMax;
    
    [SerializeField] private float spawnPositionYRange = 4.5f;
    private float spawnYTop;
    private float spawnYBottom;

    private Coroutine spawnRoutine;

    private ProgressionManager progressionManager;

    void Start()
    {
        progressionManager = FindAnyObjectByType<ProgressionManager>();

        spawnYTop = transform.position.y + spawnPositionYRange;
        spawnYBottom = transform.position.y - spawnPositionYRange;

        spawnRoutine = StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            currentSpawnAmountMax = Mathf.Clamp
                (
                    (int)(progressionManager.NormalizedDistance * allTimeSpawnAmountMax),
                    allTimeSpawnAmountMin,
                    allTimeSpawnAmountMax
                );

            int amountToSpawn = Random.Range(allTimeSpawnAmountMin, currentSpawnAmountMax);
            for (int i = 0; i <= amountToSpawn; i++)
            {
                var prefab = GetWeightedRandomPrefab();
            
                PoolManager.Instance.InstantiateFromPool
                    (
                    prefab, 
                    new(spawnXPosition.position.x, Random.Range(spawnYBottom, spawnYTop), transform.position.z), 
                    Quaternion.identity
                    );
            }

            float invertedNormalizedDistance = 1f - progressionManager.NormalizedDistance;
            float currentSpawnIntervalMin = Mathf.Lerp(spawnIntervalMin, spawnIntervalMax, invertedNormalizedDistance);
            var spawnInterval = Random.Range(currentSpawnIntervalMin, spawnIntervalMax);

            /*Debug.Log($"current maximum spawn amount: {currentSpawnAmountMax};" +
                $" spawned amount: {amountToSpawn};" +
                $" minimum spawn interval: {currentSpawnIntervalMin};" +
                $" spawn interval: {spawnInterval}");*/

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private GameObject GetWeightedRandomPrefab()
    {
        float progression = progressionManager.NormalizedDistance;
        float totalWeight = 0f;

        float[] weights = new float[m_Prefabs.Length];
        for (int i = 0; i < m_Prefabs.Length; i++)
        {
            weights[i] = Mathf.Lerp(1f - progression, progression, (float)i / (m_Prefabs.Length - 1));
            totalWeight += weights[i];
        }

        float randomValue = Random.Range(0, totalWeight);

        float cumulativeWeight = 0f;
        
        for (int i = 0; i < m_Prefabs.Length; i++)
        {
            cumulativeWeight += weights[i];
            if (randomValue <= cumulativeWeight)
            {
                return m_Prefabs[i];
            }
        }

        return m_Prefabs[0];
    }
}
