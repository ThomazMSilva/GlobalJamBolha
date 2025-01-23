using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class AirBubbleSpawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private GameObject[] bubble;
        [SerializeField] private float minSpawnInterval = 3f;
        [SerializeField] private float maxSpawnInterval = 9f;
        [SerializeField] private float minSpawnRange = .3f;
        [SerializeField] private float maxSpawnRange = 5f;
        [SerializeField] private int minSpawnAmount = 1;
        [SerializeField] private int maxSpawnAmount = 3;

        private List<GameObject> spawnedBubbles = new();
        private Coroutine spawnCoroutine;
        private ProgressionManager progressionManager;

        private void OnEnable()
        {
            spawnCoroutine ??= StartCoroutine(Spawn());
        }

        private void OnDisable()
        {
            spawnedBubbles.Clear();
            spawnCoroutine = null;
        }

        private void Start()
        {
            progressionManager = FindAnyObjectByType<ProgressionManager>();
        }

        private void Update()
        {
            transform.position = new
                (
                    transform.position.x - (progressionManager.BaseMPS * progressionManager.ProgressionMultiplier) * Time.deltaTime,
                    transform.position.y,
                    transform.position.z
                );
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Finish")) gameObject.SetActive(false);
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                float spawnRange = Random.Range(minSpawnRange, maxSpawnRange);
                for (int i = 0; i < Random.Range(minSpawnAmount, maxSpawnAmount + 1); i++) 
                {
                    Vector3 position;
                    bool validPosition;

                    do
                    {
                        validPosition = true;
                        position = new
                            (
                                Random.Range(spawnPosition.position.x - spawnRange, spawnPosition.position.x + spawnRange),
                                spawnPosition.position.y,
                                spawnPosition.position.z
                            );

                        foreach (var bubbleInstance in spawnedBubbles)
                        {
                            if (Vector3.Distance(bubbleInstance.transform.position, position) < bubbleInstance.GetComponent<Collider>().bounds.size.x)
                            {
                                validPosition = false;
                                break;
                            }
                        }
                        yield return null;
                    }
                    while (!validPosition);

                    var newBubble = PoolManager.Instance.InstantiateFromPool(bubble[Random.Range(0, bubble.Length)], position, Quaternion.identity);
                    spawnedBubbles.Add(newBubble);
                }

                yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
            }
        }
    }
}