using Subvrsive.Combat.Manager;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    [SerializeField] LeaderboardUI leaderboardUI;
    [SerializeField] GameObject startUI;

    private void Start()
    {
        EventManager.OnGameOver += OnGameOver;
        startUI.SetActive(true);
    }

    private void OnDestroy()
    {
        EventManager.OnGameOver += OnGameOver;
    }


    void OnGameOver()
    {
        // Show Game Over UI
        Debug.Log("Game Over! Show Game Over UI");
        if(leaderboardUI == null)
        {
            Debug.LogError("Leaderboard UI is not assigned in the inspector.");
            return;
        }
        leaderboardUI.gameObject.SetActive(true);
        string winner = GameManager.Instance.GetWinnerName();
        leaderboardUI.SetData(GameManager.Instance.characterStatsDictionary, winner);
        leaderboardUI.Initialize();
    }

    public void Onclick_Restart()
    {
        // Restart the game
        Debug.Log("Restarting Game");
        SceneManager.Instance.Restart();
    }

    public void Onclick_Quit()
    {
        // Quit the game
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void OnClick_StartMatch()
    {
        // Start the match
        Debug.Log("Starting Match");
        EventManager.Event_OnGameStart();
        startUI.SetActive(false);
    }
}
