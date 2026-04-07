using UnityEngine;

public class FinishLine : MonoBehaviour
{
    // during playtesting it was revealed the finish line could be triggered twice, not anymore
    private bool _triggered = false;
    // when the player goes into the winning zone, show the end game ui
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_triggered)
        {
            _triggered = true;
            GameManager.instance.UpdateGameState(GameStates.END);
        }
    }
}
