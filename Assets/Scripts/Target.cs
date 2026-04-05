using UnityEngine;

public class Target : MonoBehaviour, IBombable
{
    [SerializeField] private int value;
    // when the target gets bombed, the target updates the score and destroys the target
    public void OnBombed()
    {
        Debug.Log("bombed");
        GameManager.instance.UpdateScore(value);
        Destroy(gameObject);
        value = 0;
    }
}
