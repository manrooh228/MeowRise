using UnityEngine;
using UnityEngine.UI;

public class CatnipFarm : MonoBehaviour
{
    public float health = 500f;
    public float maxHealth = 500f;
    
    [SerializeField] public HealthBar healthBar;
    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.SetHealth(health, maxHealth);
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Ферму атакуют! Осталось ХП: " + health);

        if (healthBar != null)
        {
            healthBar.SetHealth(health, maxHealth);
        }


        if (health <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Ферма разрушена! Конец игры.");
        Time.timeScale = 0; // Остановка игры
    }
}
