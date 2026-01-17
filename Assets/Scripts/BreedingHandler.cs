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
        [SerializeField] private GameObject catPrefab; // Префаб котенка (квадратик)
        [SerializeField] private Transform spawnPoint; // Место появления
        [SerializeField] private int kittenCount = 0;
        [SerializeField] private List<Cat> kittens;
        public Transform[] kittenPockets;
        private int currentPocketIndex = 0;

        void Start()
        {
            father.Init(100f, 1.5f, true, true);
            mother.Init(150f, 1.0f, true, false); 

            StartCoroutine(BreedingCycle());
        }

        IEnumerator BreedingCycle()
        {
            while (true)
            {
                if(kittenCount <= 6)
                {
                    yield return new WaitForSeconds(5f);
                    SpawnKitten();
                }
            }
        }

        void SpawnKitten()
        {
            if (currentPocketIndex >= kittenPockets.Length)
            {
                Debug.Log("Все карманы заняты!");
                return;
            }

            Transform targetPocket = kittenPockets[currentPocketIndex];

            GameObject newKittenObj = Instantiate(catPrefab, targetPocket);

            newKittenObj.transform.localPosition = new Vector2(0, 0);

            Cat kitten = newKittenObj.GetComponent<Cat>();

            currentPocketIndex++;


            //Логика наследования 70/30
            //70/30 хп от папы
            float kittenDamage = (father.damageMultiplier * 0.7f) + (mother.damageMultiplier * 0.3f);

            //70/30 хп от мамы
            float kittenHealth = (mother.health * 0.7f) + (father.health * 0.3f);

            //random +-10%
            kittenDamage *= Random.Range(0.9f, 1.1f);
            kittenHealth *= Random.Range(0.9f, 1.1f);

            //Убрать .0
            kittenHealth = Mathf.Floor(kittenHealth);

            kitten.Init(kittenHealth, kittenDamage, false, GenderHandler());

            Debug.Log($"Родился котенок! HP: {kittenHealth:F1}, DMG: {kittenDamage:F1}");

            kittens.Add(kitten);
            kittenCount++;
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