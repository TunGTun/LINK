using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBridgeController : MonoBehaviour
{
    public GameObject Gift;

    void Update()
    {
        this.SpawnResult();
    }
    public void SpawnResult()
    {

        if (CountCollectItem.itemCount == 3 && Gift != null)
        {
            Gift.SetActive(true);
            
        }
    }
}
