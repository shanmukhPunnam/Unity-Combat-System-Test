using System.Collections.Generic;
using UnityEngine;

namespace Subvrsive.Combat.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Game Settings")]
        [SerializeField] private GameObject characterPrefab;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private int numberOfCharactersToSpawn = 10;
        [SerializeField] private Transform charactersPawnParrent;

        [Header("Game State")]
        public List<GameObject> characters = new List<GameObject>();
        public List<GameObject> liveCharacters = new List<GameObject>();

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            SpawnCharacters();
            liveCharacters.AddRange(characters);
        }

        private void SpawnCharacters()
        {
            for (int i = 0; i < numberOfCharactersToSpawn; i++)
            {
                GameObject character = Instantiate(characterPrefab, GetSpawnPosition(i), Quaternion.identity);
                character.transform.SetParent(charactersPawnParrent);
                character.name = "Character_" + i;
                characters.Add(character);
            }
        }

        private Vector3 GetSpawnPosition(int characterIndex)
        {
            if (spawnPoints.Length == 0)
            {
                Debug.LogError("No spawn points available!");
                return Vector3.zero;
            }
            if (spawnPoints.Length < characterIndex)
            {
                Debug.LogError("Not enough spawn points for the number of characters!");
                return spawnPoints[0].position;
            }

            Vector3 spawnPosition = spawnPoints[characterIndex].position;
            return spawnPosition;
        }

        //set gizmo for spawnpoinrts
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            foreach (Transform spawnPoint in spawnPoints)
            {
                Gizmos.DrawSphere(spawnPoint.position, 0.5f);
            }
        }


        public void OnCharacterDie(GameObject character)
        {
            if (liveCharacters.Contains(character))
            {
                liveCharacters.Remove(character);
            }
            if (liveCharacters.Count == 0)
            {
                Debug.Log("All characters are dead!");
                // Handle game over logic here
            }

            if(liveCharacters.Count == 1)
            {
                Debug.Log($"GameOver : {liveCharacters[0].name} is win the match");
                // Handle game over logic here
            }
        }

        public GameObject GetRandomLiveEnimy()
        {
            //return null;

            if (liveCharacters.Count == 0)
            {
                Debug.LogError("No live characters available!");
                return null;
            }
            int randomIndex = Random.Range(0, liveCharacters.Count);
            GameObject randomCharacter = liveCharacters[randomIndex];
            if (randomCharacter != null)
            {
                return randomCharacter;
            }
            else
            {
                Debug.LogError("Random character is null!");
                return null;
            }
        }
    }
}



