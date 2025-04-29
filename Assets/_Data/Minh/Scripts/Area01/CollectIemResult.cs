using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectIemResult : MonoBehaviour
{
    public GameObject[] Bridges;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player"))
        {
            
            foreach (GameObject Bridge in Bridges)
            {
                Bridge.SetActive(true);
            }
            Destroy(gameObject,0.1f);
        }

    }


}