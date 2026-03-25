using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject endCanvas;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;
    // a function to load the course when the start button is pressed
    public void StartGame()
    {
        SceneManager.LoadScene("Course1");
    }
    // a function to close the game when the quit button is pressed
    public void CloseGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game, if you are seeing this the game should have quit already");
    }
    // a function to pause and unpause the game when the pause button is pressed
    // it pulls the game state enum from the game manager script to know what should be done off of one button press
    // its main function in game is to give the player time to decide what to do when the wonky physics are wonky, it also lets you spawn a lot of bombs while time is frozen and that can be quite fun
    public void PauseUnpauseGame()
    {
        if (gameManager == null) return;
        if (gameManager.GetGameState() == GameStates.PLAY)
        {
            gameManager.UpdateGameState(GameStates.PAUSE);
            Time.timeScale = 0f;
            pauseCanvas.SetActive(true);
        }
        else if (gameManager.GetGameState() == GameStates.PAUSE)
        {
            gameManager.UpdateGameState(GameStates.PLAY);
            Time.timeScale = 1f;
            pauseCanvas.SetActive(false);
        }
    }
    // a function that is triggered by the finish line to bring up the score ui
    public void ShowScore(int Score, float Time)
    {
        if (endCanvas == null) return;
        if (scoreText == null) return;
        if (timeText == null) return;
        scoreText.SetText(Score.ToString());
        timeText.SetText(Time.ToString());
        endCanvas.SetActive(true);
    }
    // a function to return to the start menu from the pause menu or score menu
    public void ReturnToStart()
    {
        SceneManager.LoadScene("StartScene");
    }
}
