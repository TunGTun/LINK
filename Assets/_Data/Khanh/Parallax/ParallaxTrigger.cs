using UnityEngine;

public class ParallaxTrigger : MonoBehaviour
{
    public ParallaxController_k parallaxController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Bắt đầu đổi texture!");
            parallaxController.ChangeParallaxTexture(); // Gọi hàm đổi nền
        }
    }
}
