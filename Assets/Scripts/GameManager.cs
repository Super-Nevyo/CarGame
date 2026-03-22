using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _score = 0;
    private GameStates _currentGameState;
    private float _startTime;
    void Start()
    {
        _score = 0;
        _currentGameState = GameStates.PLAY;
        _startTime = Time.time;
    }

    void Update()
    {
        
    }

    public void UpdateScore(int ScoreChanged)
    {
        _score += ScoreChanged;
        Debug.Log(_score);
    }
    public void UpdateGameState(GameStates UpdateTo)
    {
        _currentGameState = UpdateTo;
    }
    public GameStates GetGameState()
    {
        return _currentGameState;
    }
    public int GetScore() { return _score; }
    public float GetTime() { return Time.time - _startTime; }
}
