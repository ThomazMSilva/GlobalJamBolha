using Assets.Scripts;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private Transform supportRod;
    private ProgressionManager progressionManager;
    private Vector3 supportRodPosition;

    [SerializeField] private float horizontalSpeed = 6f;
    [SerializeField] private float verticalSpeed = 15f;

    [SerializeField] private float horizontalRange = 2f;
    [SerializeField] private float verticalRange = 10f;
    [SerializeField] private float rangeOffset = .1f;
    private float horizontalMinRange;
    private float horizontalMaxRange;
    private float verticalMinRange;
    private float verticalMaxRange;


    private float horizontalInput;
    private float verticalInput;
    private readonly float speedMultiplier = 10f;


    //So pra ler no modo Debug
    private Vector3 velocity;

    private void Start()
    {
        progressionManager = FindAnyObjectByType<ProgressionManager>();
        supportRodPosition = supportRod.position;
        horizontalMinRange = supportRod.position.x - horizontalRange;
        horizontalMaxRange = supportRod.position.x + horizontalRange;
        verticalMinRange = supportRod.position.y - verticalRange;
        verticalMaxRange = supportRod.position.y + verticalRange;
    }

    private void Update()
    {
        if (supportRodPosition != supportRod.position) 
        {
            horizontalMinRange = supportRod.position.x - horizontalRange;
            horizontalMaxRange = supportRod.position.x + horizontalRange;
            verticalMinRange = supportRod.position.y - verticalRange;
            verticalMaxRange = supportRod.position.y + verticalRange;

            supportRodPosition = supportRod.position;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        playerRb.velocity = new
            (
                horizontalInput * speedMultiplier * (progressionManager.BaseMPS * progressionManager.ProgressionMultiplier) * horizontalSpeed * Time.fixedDeltaTime, 
                verticalInput * speedMultiplier * (progressionManager.BaseMPS * progressionManager.ProgressionMultiplier) * verticalSpeed * Time.fixedDeltaTime,
                0
            );
        velocity = playerRb.velocity;

        //Clamp maligno
        if (playerRb.position.y > verticalMaxRange)
        {
            playerRb.velocity = new(playerRb.velocity.x, 0, 0);
            playerRb.position = new(playerRb.position.x, verticalMaxRange - rangeOffset, 0);
        }
        else if (playerRb.position.y < verticalMinRange) 
        {
            playerRb.velocity = new(playerRb.velocity.x, 0, 0);
            playerRb.position = new(playerRb.position.x, verticalMinRange + rangeOffset, 0);
        }

        if (playerRb.position.x > horizontalMaxRange)
        {
            playerRb.velocity = new(0, playerRb.velocity.y, 0);
            playerRb.position = new(horizontalMaxRange - rangeOffset, playerRb.position.y, 0);
        }
        else if(playerRb.position.x < horizontalMinRange)
        {
            playerRb.velocity = new(0, playerRb.velocity.y, 0);
            playerRb.position = new(horizontalMinRange + rangeOffset, playerRb.position.y, 0);
        }


        /*playerRb.position = new
            (
                Mathf.Clamp(playerRb.position.x, -horizontalMaxRange, horizontalMaxRange),
                Mathf.Clamp(playerRb.position.y, -verticalMaxRange, verticalMaxRange),
                0
            );*/
    }
}
