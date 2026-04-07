using UnityEngine;

public class Target : MonoBehaviour, IBombable
{
    [SerializeField] private int value;
    // when the target gets bombed, the target updates the score and destroys the target
    public void OnBombed()
    {
        GameManager.instance.UpdateScore(value);
        Destroy(gameObject);
        // if you paused the game and spawned several bombs at once, the target would give a point for every bomb that hit, that does not happen anymore
        value = 0;
    }
}
