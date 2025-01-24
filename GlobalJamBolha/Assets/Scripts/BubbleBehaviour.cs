using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBehaviour : MonoBehaviour
{
    [SerializeField] private float minVerticalVelocity = .8f;
    [SerializeField] private float maxVerticalVelocity = 1.2f;
    [SerializeField] private float minBaseHorizontalVelocity = 1.1f;
    [SerializeField] private float maxBaseHorizontalVelocity = 1.5f;
    [SerializeField] private float oxygenRecovered = 25f;
    [SerializeField] private float minLifetime = 2f;
    [SerializeField] private float maxLifetime = 5f;
    private float currentRandomHorizontal;
    private float currentRandomVertical;
    private ProgressionManager progressionManager;
    private Coroutine despawnRoutine;


    private void OnEnable()
    {
        currentRandomHorizontal = Random.Range(minBaseHorizontalVelocity, maxBaseHorizontalVelocity);
        currentRandomVertical = Random.Range(minVerticalVelocity, maxVerticalVelocity);

        despawnRoutine ??= StartCoroutine(Despawn(Random.Range(minLifetime, maxLifetime)));
    }

    private void OnDisable()
    {
        if(despawnRoutine != null) StopCoroutine(despawnRoutine);
    }

    private void Start()
    {
        progressionManager =  FindAnyObjectByType<ProgressionManager>();
    }

    private void Update()
    {
        if (progressionManager.isGamePaused) gameObject.SetActive(false);

        transform.position = new
            (
                transform.position.x - (progressionManager.BaseMPS * progressionManager.ProgressionMultiplier * currentRandomHorizontal * Time.deltaTime),
                transform.position.y + (currentRandomVertical * Time.deltaTime),
                transform.position.z
            );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.TryGetComponent<OxygenManager>(out var oxygenMng))
            {
                oxygenMng.RecoverOxygen(oxygenRecovered);
            }
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Finish")) gameObject.SetActive(false);
    }

    private IEnumerator Despawn(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        despawnRoutine = null;
        Debug.Log("Despawnou sem colidir com nada");
        gameObject.SetActive(false);
    }
}
