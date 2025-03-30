using UnityEngine;

public class SkeletonTrigger : MonoBehaviour
{
    public SkeletonController skeleton;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           if (skeleton != null)
            {
                skeleton.ActivateSkeleton();  // Gọi hàm nhô lên
            }
        }
    }
}
