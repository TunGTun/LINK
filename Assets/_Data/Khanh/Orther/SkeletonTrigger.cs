using UnityEngine;

public class SkeletonTrigger : MonoBehaviour
{
    public SkeletonController skeleton;
    public SkeletonController skeleton1;
    public SkeletonController skeleton2;
    public SkeletonController skeleton3;
    public SkeletonController skeleton4;
    public SkeletonController skeleton5;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           if (skeleton != null)
            {
                skeleton.ActivateSkeleton();  // Gọi hàm nhô lên
                skeleton1.ActivateSkeleton();
                skeleton2.ActivateSkeleton();
                skeleton3.ActivateSkeleton();
                skeleton4.ActivateSkeleton();
                skeleton5.ActivateSkeleton();  
            }
        }
    }
}
