using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] m_Prefabs;
    [SerializeField] private Transform spawnXPosition;
    [SerializeField] private float spawnIntervalMin = 1;
    [SerializeField] private float spawnIntervalMax = 4;
    
    [SerializeField] private float spawnAmountMin = 1;
    [SerializeField] private float spawnAmountMax = 4;
    
    [SerializeField] private float spawnPositionYRange = 4.5f;
    private float spawnYTop;
    private float spawnYBottom;

    private Coroutine spawnRoutine;

    void Start()
    {
        spawnYTop = transform.position.y + spawnPositionYRange;
        spawnYBottom = transform.position.y - spawnPositionYRange;

        spawnRoutine = StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            for (int i = 0; i <= Random.Range(spawnAmountMin, spawnAmountMax); i++)
            {
                var prefab = m_Prefabs[Random.Range(0, m_Prefabs.Length - 1)];
            
                PoolManager.Instance.InstantiateFromPool
                    (
                    prefab, 
                    new(spawnXPosition.position.x, Random.Range(spawnYBottom, spawnYTop), transform.position.z), 
                    Quaternion.identity
                    );
            }

            var spawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
