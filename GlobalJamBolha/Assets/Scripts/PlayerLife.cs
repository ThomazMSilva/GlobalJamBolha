using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerLife : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 3;
        [SerializeField] private int currentHealth;

        [SerializeField] private bool isInvulnerable = false;
        [SerializeField] private float invulnerabilityTime = 1.5f;
        //private WaitForSeconds waitForInvulnerability;
        private Coroutine inInvulnerability;
        [SerializeField] private Animator playerAnim;


        public int CurrentHealth
        {
            get => currentHealth;
            private set
            {
                int oldValue = currentHealth;
                currentHealth = Mathf.Clamp(value, 0, maxHealth);
                OnHealthChanged?.Invoke(this, oldValue, currentHealth);

                inInvulnerability ??= StartCoroutine(InvulnerabilityTime());

                if (oldValue > currentHealth) AdministradorSom.Instance.PlayDolphinSound();

                if (currentHealth <= 0) OnPlayerDeath?.Invoke();
            }
        }

        private void Start()
        {
            currentHealth = maxHealth;
            //waitForInvulnerability = new(invulnerabilityTime);
            OnPlayerDeath += ResetGame;
        }

        private void ResetGame()
        {
            CurePlayer(maxHealth);
            PoolManager.Instance.DeactivateAllObjects();
        }

        public void DamagePlayer(int damageAmount)
        {
            if (isInvulnerable) return;
            CurrentHealth -= damageAmount;
        }

        public void CurePlayer(int cureAmount) => CurrentHealth += cureAmount;
        
        public delegate void HealthChangedHandler(object sender, int oldHealth, int newHealth);
        public delegate void PlayerDeathHandler();

        public HealthChangedHandler OnHealthChanged;
        public PlayerDeathHandler OnPlayerDeath;

        private IEnumerator InvulnerabilityTime()
        {
            isInvulnerable = true;
            playerAnim.SetBool("isStunned", true);
            GetComponent<SpriteRenderer>().color = Color.magenta;

            yield return new WaitForSeconds(invulnerabilityTime);

            GetComponent<SpriteRenderer>().color = Color.white;

            isInvulnerable = false;

            playerAnim.SetBool("isStunned", false);

            inInvulnerability = null;
        }
    }
}