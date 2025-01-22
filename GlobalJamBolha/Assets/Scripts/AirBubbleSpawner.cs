using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class AirBubbleSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] bubble;
        [SerializeField] private float minSpawnInterval = 3f;
        [SerializeField] private float maxSpawnInterval = 9f;
        [SerializeField] private float minSpawnRange = .3f;
        [SerializeField] private float maxSpawnRange = 5f;
        [SerializeField] private int minSpawnAmount = 2;
        [SerializeField] private int maxSpawnAmount = 5;

        private void Start()
        {
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            while (true)
            {

                yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));

                float spawnRange = Random.Range(minSpawnRange, maxSpawnRange);
                for (int i = 0; i < Random.Range(minSpawnAmount, maxSpawnAmount + 1); i++) 
                {
                    Vector3 spawnPosition = new(Random.Range(transform.position.x - spawnRange, transform.position.x + spawnRange), transform.position.y, transform.position.z);
                    PoolManager.Instance.InstantiateFromPool(bubble[Random.Range(0, bubble.Length)], spawnPosition, Quaternion.identity);
                }
            }
        }
    }
}