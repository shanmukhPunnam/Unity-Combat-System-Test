using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro; // For sorting

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] GameObject leaderboardEntryPrefab;
    [SerializeField] Transform leaderboardContainer;

    [SerializeField] TextMeshProUGUI winnerText;

    private List<CharacterStatsData> sortedStatsList = new List<CharacterStatsData>();
    string matchWinnerName;
    /// <summary>
    /// Accepts dictionary data and sorts it by kill count before displaying it.
    /// </summary>
    public void SetData(Dictionary<string, CharacterStatsData> characterStatsDictionary, string winnerName)
    {
        if(string.IsNullOrEmpty(winnerName) == false)
        {
            winnerText.text = "Last One Standing: " + winnerName;
        }

        matchWinnerName = winnerName;
        // Sort by KillCount descending
        sortedStatsList = characterStatsDictionary
            .Select(kvp => kvp.Value)
            .OrderByDescending(stat => stat.KillCount)
            .ToList();
    }

    /// <summary>
    /// Builds the leaderboard UI from the sorted stats list.
    /// </summary>
    public void Initialize()
    {
        // Clear existing children (in case of refresh)
        foreach (Transform child in leaderboardContainer)
        {
            Destroy(child.gameObject);
        }

        // Instantiate leaderboard UI entries
        foreach (var characterStat in sortedStatsList)
        {
            GameObject entryGO = Instantiate(leaderboardEntryPrefab, leaderboardContainer);
            LeaderboardUIItem leaderboardUIItem = entryGO.GetComponent<LeaderboardUIItem>();
            bool isWinner = false;
            if (string.IsNullOrEmpty(matchWinnerName) == false & characterStat.characterName == matchWinnerName)
            {
                isWinner = true;
            }
            leaderboardUIItem.SetData(characterStat, isWinner);

            // Optional: Debug log
            Debug.Log($"Character: {characterStat.characterName}, Kills: {characterStat.KillCount}");
        }
    }
}
