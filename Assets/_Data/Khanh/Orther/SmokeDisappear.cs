using UnityEngine;
using System.Collections;

public class SmokeDisappear : MonoBehaviour
{
    private Animator anim;
    private bool isSteppedOn = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isSteppedOn)
        {
            isSteppedOn = true;
            anim.SetTrigger("Disappear"); // Chỉ khi Player dẫm lên mới chạy "FadeOut"
        }
    }

    public void DestroySmoke()
    {
        Destroy(gameObject); // Gọi từ Animation Event ở cuối "FadeOut"
    }
}
