using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TNTExplosion : MonoBehaviour
{
    public Animator explosionAnimator;
    public GameObject[] wheels;
    public float wheelSpeed = 5f;
    public float explosionDelay = 0.5f;

    private void Start()
    {
        // Đảm bảo bánh xe đứng yên lúc đầu
        foreach (GameObject wheel in wheels)
        {
            Rigidbody2D rb = wheel.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Static;
            }
        }
    }

    public void StartBoom()
    {
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        explosionAnimator.SetTrigger("Explode"); // Kích hoạt animation vụ nổ
        yield return new WaitForSeconds(explosionDelay);

        foreach (GameObject wheel in wheels)
        {
            Rigidbody2D rb = wheel.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.velocity = new Vector2(wheelSpeed, 0);
            }
        }
        Destroy(gameObject); // Hủy TNT sau khi nổ
    }
}
