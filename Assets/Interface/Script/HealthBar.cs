using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI healthText;

    public void SetHealth(float currentHealth, float maxHealth)
    {
        if (healthText == null)
        {
            Debug.LogError("Внимание! Не назначены ссылки в HealthBar на объекте: " + gameObject.name);
            return;
        }

        float displayHP = Mathf.Max(0, currentHealth);
        healthText.text = $"{displayHP} / {maxHealth}";
    }
}
