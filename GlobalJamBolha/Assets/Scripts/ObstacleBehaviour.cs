using Assets.Scripts;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private int damage;

    [SerializeField] private bool progressSpeedWithDistanceRan = true;

    private ProgressionManager progressionManager;

    private void Start() => progressionManager = FindAnyObjectByType<ProgressionManager>();

    private void Update()
    {
        transform.Translate((progressSpeedWithDistanceRan ? progressionManager.ProgressionMultiplier : 1) * speed * Time.deltaTime * -Vector3.right);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish")) gameObject.SetActive(false);

        if (other.gameObject.CompareTag("Player")) 
        {
            Debug.Log("Colodiu com jogrfoi");
            if (!other.TryGetComponent<PlayerLife>(out PlayerLife playerLife)) return;

            playerLife.DamagePlayer(damage);
         
            gameObject.SetActive(false);
        }
    }
}
