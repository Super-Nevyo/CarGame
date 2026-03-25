using UnityEngine;

public class Target : MonoBehaviour, IBombable
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private int value;
    // when the target gets bombed, the target updates the score and destroys the target
    public void OnBombed()
    {
        Debug.Log("bombed");
        gameManager.UpdateScore(value);
        Destroy(gameObject);
    }
}
