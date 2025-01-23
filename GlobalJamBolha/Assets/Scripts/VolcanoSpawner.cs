using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class VolcanoSpawner : MonoBehaviour
    {
        [SerializeField] private ProgressionManager progressionManager;
        [SerializeField] private GameObject volcano;
        [SerializeField] private float maxSpawnInterval = 5f;
        [SerializeField] private float minSpawnInterval = 3f;


        private void Start()
        {
            StartCoroutine(SpawnVolcano());
        }

        private IEnumerator SpawnVolcano()
        {
            while (true)
            {
                yield return new WaitForSeconds(Mathf.Lerp(maxSpawnInterval, minSpawnInterval, progressionManager.NormalizedDistance));

                PoolManager.Instance.InstantiateFromPool(volcano, transform.position, Quaternion.identity);
            }
        }
    }
}