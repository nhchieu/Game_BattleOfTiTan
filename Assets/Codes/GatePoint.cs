using UnityEngine;

public class GatePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player"))
        {
            SceneController.instance.NextScene();
        }
        
    }
}
