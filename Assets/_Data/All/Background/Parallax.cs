using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : LinkMonoBehaviour
{
    Material material;
    float distance;

    [Range(0f, 0.5f)]
    public float speed = 0.2f;

    protected override void Start()
    {
        base.Start();
        material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        distance += Time.deltaTime * speed;
        material.SetTextureOffset("_MainTex", Vector2.right *  distance);
    }
}
