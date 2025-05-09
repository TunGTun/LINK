using UnityEngine;
using System.Collections;

public class SmokeDisappear : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    private bool isSteppedOn = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isSteppedOn)
        {
            isSteppedOn = true;
            anim.SetBool("Disappear",true);
        }
    }

    public void HideTemporarily()
    {
        StartCoroutine(HideAndShow());
    }

    private IEnumerator HideAndShow()
    {
        spriteRenderer.enabled = false;
        col.enabled = false;

        yield return new WaitForSeconds(1f);

        anim.SetBool("Disappear", false);
        spriteRenderer.enabled = true;
        col.enabled = true;
        isSteppedOn = false; 
    }
}
