using System.Collections;
using UnityEngine;

namespace Assets.Scripts.BattleSystem
{
    public class AntSpawner : MonoBehaviour
    {
        public GameObject antPrefab;
        public Transform spawnPointAnt;
        public Transform battleMap;
        public int currentLevel = 1;
        public float spawnInterval; //время создания

        void Start()
        {
            InvokeRepeating("SpawnAnt", 2f, spawnInterval);
            InvokeRepeating("IncreaseLevel", 60f, 60f);
        }

        void SpawnAnt()
        {
            GameObject newAnt = Instantiate(antPrefab, spawnPointAnt.position, Quaternion.identity, battleMap);
            
            newAnt.GetComponent<Ant>().Init(currentLevel);
            newAnt.tag = "Ant";
        }

        void IncreaseLevel()
        {
            currentLevel++;
            Debug.Log("Уровень муравьев повысился: " + currentLevel);
        }
    }
}