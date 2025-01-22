using Assets.Scripts;
using UnityEngine;

public class OxygenManager : MonoBehaviour
{
    [SerializeField] private PlayerLife playerLife;
    private float currentOxygen;
    [SerializeField] private float fullOxygen = 100;
    [SerializeField] private int oxygenDamage = 1;
    [SerializeField] private float oxygenConsumptionSpeed = 1.5f;

    [SerializeField] private UnityEngine.UI.Image oxygenHUD_IMG;

    private void Start()
    {
        currentOxygen = fullOxygen;
    }

    private void Update()
    {
        currentOxygen -= Time.deltaTime * oxygenConsumptionSpeed;
        if (currentOxygen < 0) 
        {
            playerLife.DamagePlayer(oxygenDamage);
            currentOxygen = fullOxygen;
        }
        oxygenHUD_IMG.fillAmount = Mathf.Clamp01(currentOxygen / fullOxygen);
    }

    public void RecoverOxygen(float oxygenAmount) => currentOxygen += oxygenAmount;

    public void DecreaseOxygen(float oxygenAmount) => currentOxygen -= oxygenAmount;
}
