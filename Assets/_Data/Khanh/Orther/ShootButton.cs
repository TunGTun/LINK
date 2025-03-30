using UnityEngine;

public class ShootButton : MonoBehaviour
{
    public StatueShooter statue; // Gán tượng vào đây
    public StatueShooter statue1;
    private bool hasActivated = false; // Biến kiểm tra đã kích hoạt chưa

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasActivated && other.CompareTag("Player"))
        {
            statue.StartShooting();
            statue1.StartShooting();
            hasActivated = true; // Chỉ kích hoạt một lần
        }
    }
}
