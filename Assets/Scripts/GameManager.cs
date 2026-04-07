using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _score = 0;
    private GameStates _currentGameState;
    private float _startTime;
    public static GameManager instance;
    void Awake()
    {
        if (instance == null) instance = this;
        _score = 0;
        UpdateGameState(GameStates.PLAY);
        _startTime = Time.time;
    }

// a function so score objects can update the score by running this script
    public void UpdateScore(int ScoreChanged)
    {
        _score += ScoreChanged;
        Debug.Log(_score);
    }
    // a function to let other scripts update the game state, used by the ui manager for pausing game and the finish line for winning
    public void UpdateGameState(GameStates UpdateTo)
    {
        _currentGameState = UpdateTo;
        if (_currentGameState == GameStates.PLAY) Time.timeScale = 1f;
        if (_currentGameState == GameStates.PAUSE) Time.timeScale = 0f;
        if (_currentGameState == GameStates.END) UiManager.instance.ShowScore(instance.GetScore(), instance.GetTime());
    }
    // used by the ui manager to tell if the game should be paused or unpaused
    public GameStates GetGameState()
    {
        return _currentGameState;
    }
    // both of these are used by the UI manager to show the score when the finish line tells it to
    public int GetScore() { return _score; }
    public float GetTime() { return Time.time - _startTime; }
}
