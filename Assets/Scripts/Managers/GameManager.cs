using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int numberOfCharactersToSpawn = 10;
    [SerializeField] private Transform charactersPawnParrent;

    [Header("Game State")]
    public List<GameObject> characters = new List<GameObject>();


    private void Start()
    {
        SpawnCharacters();
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
        if(spawnPoints.Length < characterIndex)
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
}

