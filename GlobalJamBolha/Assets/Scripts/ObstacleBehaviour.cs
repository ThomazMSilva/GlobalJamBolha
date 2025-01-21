using Assets.Scripts;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private int damage;

    private void Update()
    {
        transform.Translate(-Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish")) gameObject.SetActive(false);

        if (other.gameObject.CompareTag("Player")) 
        {
            Debug.Log("Colodiu com jogrfoi");
            if (!other.TryGetComponent<PlayerLife>(out PlayerLife playerLife)) return;

            playerLife.DamagePlayer(damage);
        }
    }
}
