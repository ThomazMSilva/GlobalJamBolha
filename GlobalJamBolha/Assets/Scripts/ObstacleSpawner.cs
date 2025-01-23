using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
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

    //private Coroutine spawnRoutine;

    private ProgressionManager progressionManager;

    void Start()
    {
        progressionManager = FindAnyObjectByType<ProgressionManager>();

        spawnYTop = transform.position.y + spawnPositionYRange;
        spawnYBottom = transform.position.y - spawnPositionYRange;

        /*spawnRoutine = */StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            while (progressionManager.DistanceRan < 100) yield return null;

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

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    /*private GameObject GetWeightedRandomPrefab()
    {
        float progression = progressionManager.NormalizedDistance;
        float totalWeight = 0f;

        float[] weights = new float[m_Prefabs.Length];
        
            string weightsStr = "";

        for (int i = 0; i < m_Prefabs.Length; i++)
        {
            weights[i] = Mathf.Pow(1 - Mathf.Abs((float)i / (m_Prefabs.Length - 1) - progression), 2);
            //weights[i] = Mathf.Lerp(1f - progression, progression, (float)i / (m_Prefabs.Length - 1));
            weightsStr += $"weight {i}: {weights[i]}\n";
            totalWeight += weights[i];
        }

        float randomValue = Random.Range(0, totalWeight);

        float cumulativeWeight = 0f;
        string cumWeight = "Cumulative Weights:\n";

        for (int i = 0; i < m_Prefabs.Length; i++)
        {
            cumulativeWeight += weights[i];
            cumWeight += cumulativeWeight+"\n";
            if (randomValue <= cumulativeWeight)
            {
                Debug.Log($"total weight: {totalWeight}; random value: {randomValue};\n{weightsStr};\n\n{cumWeight}\nspawned {i}");
                return m_Prefabs[i];
            }
        }

        
        return m_Prefabs[0];
    }*/
    
    private GameObject GetWeightedRandomPrefab()
    {
        int weightedRandom = (int)((m_Prefabs.Length - 1) * TriangularDistribution(0, progressionManager.NormalizedDistance, 1));

        return m_Prefabs[weightedRandom];
    }


    //roubei da internet u/GroZZleR
    public static float TriangularDistribution(float minimum = 0f, float peak = 0.5f, float maximum = 1f)
    {
        float v = Random.value;

        if (v < (peak - minimum) / (maximum - minimum))
            return minimum + Mathf.Sqrt(v * (maximum - minimum) * (peak - minimum));
        else
            return maximum - Mathf.Sqrt((1f - v) * (maximum - minimum) * (maximum - peak));
    }
}
