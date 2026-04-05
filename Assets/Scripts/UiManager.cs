using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject endCanvas;
    [SerializeField] private GameObject titleCanvas;
    [SerializeField] private GameObject levelSelectCanvas;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;

    public static UiManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    // functions to load the courses when the level button is pressed
    public void StartLevel1()
    {
        SceneManager.LoadScene("Course1");
    }
    public void StartLevel2()
    {
        SceneManager.LoadScene("Course2");
    }
    // functions to navagate the start scene
    public void StartPressed()
    {
        titleCanvas.SetActive(false);
        levelSelectCanvas.SetActive(true);
    }
    public void BackPressed()
    {
        titleCanvas.SetActive(true);
        levelSelectCanvas.SetActive(false);
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
        if (GameManager.instance.GetGameState() == GameStates.PLAY)
        {
            GameManager.instance.UpdateGameState(GameStates.PAUSE);
            pauseCanvas.SetActive(true);
        }
        else if (GameManager.instance.GetGameState() == GameStates.PAUSE)
        {
            GameManager.instance.UpdateGameState(GameStates.PLAY);
            pauseCanvas.SetActive(false);
        }
    }
    // a function that is triggered by the finish line to bring up the score ui
    public void ShowScore(int Score, float Time)
    {
        if (endCanvas == null) SceneManager.LoadScene("StartScene");
        scoreText.SetText(Score.ToString());
        timeText.SetText(Time.ToString());
        pauseCanvas.SetActive(false);
        endCanvas.SetActive(true);
    }
    // a function to return to the start menu from the pause menu or score menu
    public void ReturnToStart()
    {
        SceneManager.LoadScene("StartScene");
    }
}
