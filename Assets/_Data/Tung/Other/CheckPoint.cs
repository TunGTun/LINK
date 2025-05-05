using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : LinkMonoBehaviour
{
    public CharCtrl _charCtrl;
    public Transform respawnPoint;
    public Collider2D coll;
    public Animator anim;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip checkPointClip;

    protected override void Start()
    {
        base.Start();
        _charCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<CharCtrl>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _charCtrl.CharStats.UpdateCheckpoint(respawnPoint.position);
            coll.enabled = false;
            audioSource.PlayOneShot(checkPointClip);
            anim.SetBool("isChecked", true);
        }
    }
}
