using UnityEngine;

public class Target : MonoBehaviour, IBombable
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private int value;
    
    public void OnBombed()
    {
        Debug.Log("bombed");
        gameManager.UpdateScore(value);
        Destroy(gameObject);
    }
}
