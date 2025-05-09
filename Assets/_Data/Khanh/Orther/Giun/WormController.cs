using System.Collections;
using UnityEngine;

public class WormController : MonoBehaviour
{
    private Animator ani;
    public GameObject AttackZone;

    private bool isDie = false;
    private bool isIDE = true;
    private bool isAttack = false;

    private void Start()
    {
        AttackZone.SetActive(false);
        ani = GetComponent<Animator>();
        ani.SetBool("isIde", isIDE);
    }

    private void Update()
    {
        if (!isAttack && !isDie)
        {
            IDE();
        }
    }

    void IDE()
    {
        isIDE = true;
        ani.SetBool("isIde", isIDE);
    }

    void OnTriggerEnter2D(Collider2D other)
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

    public void Dead()
    {
        Destroy(gameObject);
    }

    public void StartAttackZone()
    {
        AttackZone.SetActive(true);
    }

    public void EndAttackZone()
    {
        AttackZone.SetActive(false);
    }

    IEnumerator StartAttack()
    {
        isAttack = true;
        isIDE = false;
        ani.SetBool("isIde", isIDE);
        ani.SetTrigger("AttackTrigger");
        yield return new WaitForSeconds(1f);

        EndAttack();
    }

    public void EndAttack()
    {
        isAttack = false;
        isIDE = true;
        ani.SetBool("isIde", isIDE);
        ani.ResetTrigger("AttackTrigger");
    }
}
