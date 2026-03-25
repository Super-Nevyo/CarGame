using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] UiManager uiManager;
    [SerializeField] GameManager gameManager;
    // when the player goes into the winning zone, show the end game ui
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.UpdateGameState(GameStates.END);
            uiManager.ShowScore(gameManager.GetScore(), gameManager.GetTime());
        }
    }
}
