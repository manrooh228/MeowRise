using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CatnipFarm : MonoBehaviour
{
    public float health = 500f;
    public float maxHealth = 500f;
    public GameObject gameoverpanel;
    
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
        // 1. Показываем панель проигрыша сразу
        if (gameoverpanel != null)
        {
            gameoverpanel.SetActive(true); // Показываем объект из иерархии
        }

        // 2. Останавливаем игровое время (физика, корутины)
        Time.timeScale = 0f;

        // 3. Запускаем специальную корутину для выхода, которая игнорирует паузу
        StartCoroutine(QuitAfterDelay(3f));

    }

    private IEnumerator QuitAfterDelay(float delay)
    {
        // Используем Realtime, так как Time.scale = 0
        yield return new WaitForSecondsRealtime(delay);

        Debug.Log("Выход из игры...");

        // Вылетаем (закрываем приложение)
        Application.Quit();

        // Если вы тестируете в редакторе Unity, эта строка остановит режим игры
        
    }
}
