using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WormController : MonoBehaviour
{
    private Animator ani;

    private bool isDie = false;
    private bool isIDE = true;
    private bool isAttack = false;

    private void Start()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isAttack&&!isDie)
        {
            IDE();
        }
    }

    void IDE()
    {
        isIDE=true;
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(StartAttack());
        }
        if (other.CompareTag("Wheel"))
        {
            Die();
        }
    }

    void Die()
    {
        isDie = true;
        ani.SetTrigger("DieTrigger");
    }

    IEnumerator StartAttack()
    {
        isAttack = true;
        isIDE = false;
        ani.SetBool("isIde", false);
        ani.SetTrigger("AttackTrigger");
        yield return new WaitForSeconds(1f);

        EndAttack();
    }

    public void EndAttack()
    {
        isAttack=false;
        isIDE = true;
        ani.SetBool("isIde", true);
        ani.ResetTrigger("AttackTrigger");
    }
}
