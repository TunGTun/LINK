using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBoom : MonoBehaviour
{
    public TNTExplosion tnt;
    private int i = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&i==1)
        {
            i--;
            tnt.StartBoom();
        }
    }
}
