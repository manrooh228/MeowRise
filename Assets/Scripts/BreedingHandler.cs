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
        
        //[SerializeField] private int kittenCount = 0;
        //[SerializeField] private List<Cat> kittens;
        public Transform[] kittenPockets;
        //private int currentPocketIndex = 0;

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
                // Проверяем, есть ли хотя бы одно свободное место
                if (GetFirstFreePocket() != null)
                {
                    yield return new WaitForSeconds(5f);
                    SpawnKitten();
                }
                else
                {
                    // Если мест нет, просто ждем секунду и проверяем снова
                    yield return new WaitForSeconds(1f);
                }
            }
        }

        void SpawnKitten()
        {
            Transform targetPocket = GetFirstFreePocket();

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
            
            kitten.Init(kittenHealth, kittenDamage, false, GenderHandler());

            kitten.ChangeName(kitten.catName);

            //Debug.Log($"Родился котенок! HP: {kittenHealth:F1}, DMG: {kittenDamage:F2}, IsMale: {GenderHandler()}");

            //kittens.Add(kitten);
            //kittenCount++;
        }
        private Transform GetFirstFreePocket()
        {
            foreach (Transform pocket in kittenPockets)
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