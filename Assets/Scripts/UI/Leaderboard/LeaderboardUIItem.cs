using TMPro;
using UnityEngine;

public class LeaderboardUIItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private GameObject WinnerTag;

    public void SetData(CharacterStatsData statsData, bool isWinner)
    {
        playerNameText.text = statsData.characterName;
        playerScoreText.text = statsData.KillCount.ToString();

        if (isWinner)
        {
            WinnerTag.SetActive(true);
        }
        else
        {
            WinnerTag.SetActive(false);
        }
    }
}
