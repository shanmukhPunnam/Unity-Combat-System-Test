using Subvrsive.Combat.Characters;
using Subvrsive.Combat.Event;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private HealthbarUI healthbarPrefab;

        [Header("Game State")]
        public List<GameObject> characters = new List<GameObject>();
        public List<GameObject> liveCharacters = new List<GameObject>();


        [Header("Progress")]
        public Dictionary<string, CharacterStatsData> characterStatsDictionary = new Dictionary<string, CharacterStatsData>();

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            EventManager.OnCharacterDie += OnCharacterDie;
            EventManager.OnGameStart += OnGameStart;
        }

        private void OnDestroy()
        {
            EventManager.OnCharacterDie -= OnCharacterDie;
            EventManager.OnGameStart -= OnGameStart;
        }

        void OnGameStart()
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
                character.name = "Player_" + i;
                characters.Add(character);

                HealthbarUI healthbar = Instantiate(healthbarPrefab, character.transform.position, Quaternion.identity);
                healthbar.Initialize(character.transform, character.GetComponent<CharacterHealth>());
                healthbar.transform.SetParent(charactersPawnParrent);

                
                CharacterStatsData characterStatsData = new CharacterStatsData
                {
                    characterName = character.name,
                    KillCount = 0
                };
                character.GetComponent<CharacterStats>().characterStatsData = characterStatsData;
                characterStatsDictionary.Add(character.name, characterStatsData);
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


        public void OnCharacterDie(CharacterManager target, CharacterManager attacker)
        {
            if (liveCharacters.Contains(target.gameObject))
            {
                liveCharacters.Remove(target.gameObject);
            }
            if (liveCharacters.Count == 0)
            {
                Debug.Log("All characters are dead!");
                EventManager.Event_OnGameOver();
            }

            if(liveCharacters.Count == 1)
            {
                Debug.Log($"GameOver : {liveCharacters[0].name} is win the match");
                EventManager.Event_OnGameOver();
            }
            string characterName = target.characterStats.characterStatsData.characterName;
            CharacterStatsData characterStats = attacker.characterStats.characterStatsData;

            if (!characterStatsDictionary.ContainsKey(characterStats.characterName))
            {
                characterStatsDictionary.Add(characterStats.characterName, characterStats);
            }
        }

        public GameObject GetRandomLiveEnimy()
        {

            if (liveCharacters.Count == 0)
            {
                Debug.LogError("No live characters available!");
                EventManager.Event_OnGameOver();

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

        public string GetWinnerName()
        {
            if (liveCharacters.Count == 1)
            {
                return liveCharacters[0].name;
            }
            else
            {
                Debug.LogError("No winner found!");
                return string.Empty;
            }
        }
    }
}



