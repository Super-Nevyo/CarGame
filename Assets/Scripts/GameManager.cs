using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Score = 0;
    void Start()
    {
        Score = 0;
    }

    void Update()
    {
        
    }

    public void UpdateScore(int ScoreChanged)
    {
        Score += ScoreChanged;
        Debug.Log(Score);
    }
}
