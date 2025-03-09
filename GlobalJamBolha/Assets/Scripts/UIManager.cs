using Assets.Scripts;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject aquariumCanvas;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelCreditos;
    [SerializeField] private GameObject painelPause;
    [SerializeField] private CinemachineVirtualCamera roomCamera;
    [SerializeField] private CinemachineVirtualCamera aquariumCamera;
    [SerializeField] private CinemachineVirtualCamera pauseCamera;
    [SerializeField] private CinemachineVirtualCamera creditsCamera;
    private bool isPaused = false;
    [SerializeField] private bool inAquarium = false;
    [SerializeField] ProgressionManager progressionManager;


    public void Start()
    {
        roomCamera.Priority = 10;
        aquariumCamera.Priority = 0;
        pauseCamera.Priority = 0;
    
        painelMenuInicial.SetActive(true);
        painelPause.SetActive(false);
        painelCreditos.SetActive(false);
    }
    public void StartGame()
    {
        aquariumCamera.Priority = 10;
        aquariumCanvas.SetActive(true);
        progressionManager.isGamePaused = false;
        inAquarium = true;
    
    }

    public void ChangeAquariumCamera()
    {    
        if (!inAquarium)
        {
            aquariumCamera.Priority = 0;
            aquariumCanvas.SetActive(false);
            progressionManager.isGamePaused = true;
        }
        else
        {
            aquariumCamera.Priority = 10;
            aquariumCanvas.SetActive(true);
            progressionManager.isGamePaused = false;
        }

        inAquarium = !inAquarium;
    }
    public void Creditos()
    {
        roomCamera.Priority = 0;
        pauseCamera.Priority = 0;
        creditsCamera.Priority = 10;

        painelMenuInicial.SetActive(false);
        painelCreditos.SetActive(true);
    }
    public void FecharCreditos()
    {
        roomCamera.Priority = 10;
        pauseCamera.Priority = 0;
        creditsCamera.Priority = 0;

        painelCreditos.SetActive(false);
        painelMenuInicial.SetActive(true);
    }
    public void SairJogo()
    {
        Debug.Log("Saiu do jogo");
        Application.Quit();
    }
    public void TogglePause()
    {
        if (!isPaused)
        {
            pauseCamera.Priority = 20;
            painelPause.SetActive(true);
            progressionManager.isGamePaused = true;
        }
        else
        {
            pauseCamera.Priority = 0;
            painelPause.SetActive(false);
            progressionManager.isGamePaused = false;
        }

        isPaused = !isPaused;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    public void ResetGame()
    {
        progressionManager.ResetProgression();
        PoolManager.Instance.DeactivateAllObjects();

        roomCamera.Priority = 10;
        aquariumCamera.Priority = 0;
        pauseCamera.Priority = 0;

        painelMenuInicial.SetActive(true);
        painelPause.SetActive(false);
        painelCreditos.SetActive(false);
    }
}    

