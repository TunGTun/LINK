using UnityEngine;


public class HealingItem : MonoBehaviour
{
    public int healAmount = 1; // Số máu hồi

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Player"))
        {
            // Tìm CharStats trong toàn bộ Character
            CharStats charStats = collision.GetComponentInChildren<CharStats>();


            charStats.AddHP(healAmount);
            Destroy(gameObject);
        }
    }


}


