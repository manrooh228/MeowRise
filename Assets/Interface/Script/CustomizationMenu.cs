using UnityEngine;

public class CustomizationMenu : MonoBehaviour
{
    public static CustomizationMenu Instance;

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Cat selectedCat;

    private void Awake()
    {
        Instance = this;
        menuPanel.SetActive(false);
    }

    public void Open(Cat targetCat)
    {
        selectedCat = targetCat;
        menuPanel.SetActive(true);
        Debug.Log($"Открыто меню для: {selectedCat.name} (HP: {selectedCat.health})");

        
    }

    public void Close()
    {
        menuPanel.SetActive(false);
    }
}
