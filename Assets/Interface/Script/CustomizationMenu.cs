using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationMenu : MonoBehaviour
{
    public static CustomizationMenu Instance;

    [Header("Interface")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text genderText;

    [Header("Cat")]
    [SerializeField] private Cat selectedCat;

    [Header("Items Images")] //в будущем класс буде
    [SerializeField] private Image hatSlot;                
    [SerializeField] private Image weaponSlot;

    private void Awake()
    {
        Instance = this;
        menuPanel.SetActive(false);
    }

    public void Open(Cat targetCat)
    {
        selectedCat = targetCat;
        menuPanel.SetActive(true);

        //Debug.Log($"ќткрыто меню дл€: {selectedCat.name} (HP: {selectedCat.health})");
        
        nameInputField.text = selectedCat.catName;
        
        ShowStatistics();
        UpdateEquipmentDisplay();
    }

    private void ShowStatistics()
    {
        float hpBonus = (selectedCat.equippedHat != null) ? selectedCat.equippedHat.healthBonus : 0;
        float dmgBonus = (selectedCat.equippedWeapon != null) ? selectedCat.equippedWeapon.damageBonus : 0;

        if (selectedCat.isMale)
            genderText.text = "Gender: Male";
        if (!selectedCat.isMale) 
            genderText.text = "Gender: Female";

        damageText.text = $"Damage: {selectedCat.damageMultiplier:F2} (+{dmgBonus})"; //покачто мултипликатор урона потом на обычный помен€ю
        healthText.text = $"Health: {selectedCat.health} (+{hpBonus})";
        //genderText.text = $"Gender: "

    }

    private void UpdateEquipmentDisplay()
    {
        if (selectedCat.equippedHat != null)
        {
            hatSlot.sprite = selectedCat.equippedHat.icon;
        }
        else hatSlot.sprite = default;

        if (selectedCat.equippedWeapon != null)
        {
            weaponSlot.sprite = selectedCat.equippedWeapon.icon;
        }
        else weaponSlot.sprite = default;
    }

    public void OnNameChanged()
    {
        if (selectedCat != null)
        {
            selectedCat.ChangeName(nameInputField.text);
        }
    }

    public void Close()
    {
        menuPanel.SetActive(false);
    }

    
}
