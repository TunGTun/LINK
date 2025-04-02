using UnityEngine;

public class ParallaxTrigger : MonoBehaviour
{
    public ParallaxController_k parallaxController; // Điều khiển hiệu ứng nền
    public GameObject exitWall; // Bức tường ngăn lối thoát
    private bool hasEnemiesRisen = false;
    private int enemyLayer;

    private void Start()
    {
        exitWall.SetActive(false); // Ban đầu tắt tường
        SkeletonController.onSkeletonRisen += EnableWall; // Lắng nghe sự kiện Rising
        enemyLayer = LayerMask.NameToLayer("Enemies"); // Lấy Layer của kẻ địch
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && parallaxController != null)
        {
            parallaxController.ChangeParallaxTexture();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && parallaxController != null)
        {
            parallaxController.ResetParallaxTexture();
        }
    }

    private void EnableWall()
    {
        hasEnemiesRisen = true;
        exitWall.SetActive(true);
    }

    private void Update()
    {
        if (hasEnemiesRisen && CountObjectsInLayer(enemyLayer) == 0)
        {
            exitWall.SetActive(false);
        }
    }

    int CountObjectsInLayer(int layer)
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        int count = 0;

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == layer)
            {
                count++;
            }
        }

        return count;
    }

    private void OnDestroy()
    {
        SkeletonController.onSkeletonRisen -= EnableWall;
    }
}
