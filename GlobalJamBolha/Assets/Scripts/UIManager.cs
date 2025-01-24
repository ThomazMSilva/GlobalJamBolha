using Assets.Scripts;
using Cinemachine;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject aquariumCanvas;
    [SerializeField] private CinemachineVirtualCamera roomCamera;
    [SerializeField] private CinemachineVirtualCamera aquariumCamera;
    [SerializeField] private bool inAquarium = false;
    [SerializeField] ProgressionManager progressionManager;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            ChangeAquariumCamera();
        }
    }

    public void ChangeAquariumCamera()
    {    
        if (!inAquarium)
        {
            aquariumCamera.Priority = 10;
            aquariumCanvas.SetActive(true);
            progressionManager.isGamePaused = false;
        }
        else
        {
            aquariumCamera.Priority = 0;
            aquariumCanvas.SetActive(false);
            progressionManager.isGamePaused = true;
        }

        inAquarium = !inAquarium;
    }
}
