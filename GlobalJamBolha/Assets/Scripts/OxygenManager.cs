using Assets.Scripts;
using UnityEngine;

public class OxygenManager : MonoBehaviour
{
    [SerializeField] private PlayerLife playerLife;
    private float currentOxygen;
    private float CurrentOxygen { get => currentOxygen; set { currentOxygen = Mathf.Clamp(value, 0, fullOxygen); } }
    [SerializeField] private float fullOxygen = 10;
    [SerializeField] private int oxygenDamage = 1;
    [SerializeField] private float oxygenConsumptionSpeed = 1.5f;

    [SerializeField] private UnityEngine.UI.Image oxygenHUD_IMG;

    private void Start()
    {
        CurrentOxygen = fullOxygen;
        playerLife.OnPlayerDeath += RecoverOxygen;
    }

    private void Update()
    {
        CurrentOxygen -= Time.deltaTime * oxygenConsumptionSpeed;
        if (CurrentOxygen < 0) 
        {
            playerLife.DamagePlayer(oxygenDamage);
            CurrentOxygen = fullOxygen;
        }
        oxygenHUD_IMG.fillAmount = Mathf.Clamp01(CurrentOxygen / fullOxygen);

        if (Input.GetKeyDown(KeyCode.P)) RecoverOxygen(fullOxygen);
        if (Input.GetKeyDown(KeyCode.O)) playerLife.CurePlayer(200);
    }

    public void RecoverOxygen() => CurrentOxygen = fullOxygen;

    public void RecoverOxygen(float oxygenAmount) => CurrentOxygen += oxygenAmount;

    public void DecreaseOxygen(float oxygenAmount) => CurrentOxygen -= oxygenAmount;
}
