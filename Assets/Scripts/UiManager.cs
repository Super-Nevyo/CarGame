using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject endCanvas;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;
    public void StartGame()
    {
        SceneManager.LoadScene("Course1");
    }
    public void CloseGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game, if you are seeing this the game should have quit already");
    }
    public void PauseUnpauseGame()
    {
        if (gameManager == null) return;
        if (gameManager.GetGameState() == GameStates.PLAY)
        {
            gameManager.UpdateGameState(GameStates.PAUSE);
            Time.timeScale = 0f;
        }
        else if (gameManager.GetGameState() == GameStates.PAUSE)
        {
            gameManager.UpdateGameState(GameStates.PLAY);
            Time.timeScale = 1f;
        }
    }
    public void ShowScore(int Score, float Time)
    {
        if (endCanvas == null) return;
        if (scoreText == null) return;
        if (timeText == null) return;
        scoreText.SetText(Score.ToString());
        timeText.SetText(Time.ToString());
        endCanvas.SetActive(true);
    }
    public void ReturnToStart()
    {
        SceneManager.LoadScene("StartScene");
    }
}
