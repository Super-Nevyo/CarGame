using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] UiManager uiManager;
    [SerializeField] GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.UpdateGameState(GameStates.END);
            uiManager.ShowScore(gameManager.GetScore(), gameManager.GetTime());
        }
    }
}
