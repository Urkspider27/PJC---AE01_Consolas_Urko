using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    //Funcion para pasarte el juego
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LevelCompleted();
            }
        }
    }
}