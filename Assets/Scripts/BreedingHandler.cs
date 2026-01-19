using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class BreedingHandler : MonoBehaviour
    {
        [SerializeField] private Cat father; // Перетащите объект PapaPocket сюда
        [SerializeField] private Cat mother; // Перетащите объект MamaPocket сюда

        [SerializeField] private GameObject motherPrefab;
        [SerializeField] private GameObject fatherPrefab;

        public Transform motherPocket;
        public Transform fatherPocket;

        private bool isChoosingParent = false;
        private bool choosingForFather;

        [SerializeField] private GameObject catPrefab; 


        // Префаб котенка (квадратик)
        
        //[SerializeField] private int kittenCount = 0;
        //[SerializeField] private List<Cat> kittens;
        public Transform[] kittenPockets;
        //private int currentPocketIndex = 0;

        private void Awake()
        {
            SetDefaultParents();
        }

        private void SetDefaultParents()
        {
            // Мама
            if (motherPocket != null && motherPrefab != null)
            {
                GameObject obj = Instantiate(motherPrefab, motherPocket);
                mother = obj.GetComponent<Cat>();
                mother.bh = this; // Назначаем ссылку на этот скрипт
                mother.Init(150f, 1.0f, true, false, mother.healthBar);
                mother.ChangeName("Мама");
            }

            // Папа
            if (fatherPocket != null && fatherPrefab != null)
            {
                GameObject obj = Instantiate(fatherPrefab, fatherPocket); // Используем fatherPrefab и fatherPocket
                father = obj.GetComponent<Cat>();
                father.bh = this; // Назначаем ссылку на этот скрипт
                father.Init(100f, 1.5f, true, true, father.healthBar);
                father.ChangeName("Папа");
            }
        }

        void Start()
        {
            StartCoroutine(BreedingCycle());
        }

        IEnumerator BreedingCycle()
        {
            while (true)
            {
                // Если мы сейчас в режиме выбора родителя, цикл "спит"
                if (isChoosingParent)
                {
                    yield return new WaitUntil(() => !isChoosingParent);
                }

                if (GetFirstFreePocket(kittenPockets) != null)
                {
                    yield return new WaitForSeconds(5f);
                    if (!isChoosingParent) SpawnKitten();
                }
                else
                {
                    yield return new WaitForSeconds(1f);
                }
            }
        }

        void SpawnKitten()
        {

            Transform targetPocket = GetFirstFreePocket(kittenPockets);

            if (targetPocket == null) return;

            GameObject newKittenObj = Instantiate(catPrefab, targetPocket);
            newKittenObj.transform.localPosition = Vector2.zero;

            Cat kitten = newKittenObj.GetComponent<Cat>();


            //Логика наследования 70/30
            //70/30 урон от папы
            float kittenDamage = (father.damageMultiplier * 0.7f) + (mother.damageMultiplier * 0.3f);

            //70/30 хп от мамы
            float kittenHealth = (mother.health * 0.7f) + (father.health * 0.3f);

            //random +-10%
            kittenDamage *= Random.Range(0.9f, 1.1f);
            kittenHealth *= Random.Range(0.9f, 1.1f);

            //Убрать .0
            kittenHealth = Mathf.Floor(kittenHealth);

            HealthBar kittenHealthBar = kitten.healthBar; 
            
            kitten.Init(kittenHealth, kittenDamage, false, GenderHandler(), kittenHealthBar);

            kitten.ChangeName(kitten.catName);

            //Debug.Log($"Родился котенок! HP: {kittenHealth:F1}, DMG: {kittenDamage:F2}, IsMale: {GenderHandler()}");

            //kittens.Add(kitten);
            //kittenCount++;
        }

        public void StartReplaceParent(bool forFather)
        {
            isChoosingParent = true;
            choosingForFather = forFather;
            Debug.Log(forFather ? "Выберите нового ПАПУ" : "Выберите новую МАМУ");

            // Здесь можно включить какой-нибудь визуальный эффект или текст "ВЫБЕРИТЕ КОТА"
        }

        public void SelectNewParent(Cat selectedCat)
        {
            if (!isChoosingParent) return;

            if (choosingForFather)
            {
                // Заменяем данные отца
                father.Init(selectedCat.health, selectedCat.damageMultiplier, true, true, father.healthBar);
                father.ChangeName(selectedCat.catName);
            }
            else
            {
                // Заменяем данные матери
                mother.Init(selectedCat.health, selectedCat.damageMultiplier, true, false, mother.healthBar);
                mother.ChangeName(selectedCat.catName);
            }

            // Удаляем котенка, который стал родителем, чтобы освободить карман
            Destroy(selectedCat.gameObject);

            isChoosingParent = false;
            Debug.Log("Родитель заменен!");
        }

        private Transform GetFirstFreePocket(Transform[] pockets)
        {
            foreach (Transform pocket in pockets)
            {
                if (pocket.childCount == 0)
                {
                    return pocket;
                }
            }
            return null;
        }

        private bool GenderHandler()
        {
            bool isMale;
            float chance = Random.value;
            if (chance < 0.5f)
            {
                isMale = true;
            }
            else 
            {   
                isMale = false;
            }
             
            return isMale;
        }
    }
}