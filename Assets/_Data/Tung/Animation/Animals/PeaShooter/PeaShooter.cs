using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : LinkMonoBehaviour
{
    public GameObject peaBulletPrefab; // Kéo prefab vào đây từ Inspector
    public Transform bulletSpawnPoint; // Vị trí bắn ra đạn
    public Transform bulletHolder;     // GameObject holder chứa các viên đạn
    public float shootInterval = 2f;   // Thời gian giữa các lần bắn
    public Animator peaAnim;

    private List<GameObject> bulletPool = new List<GameObject>();
    private float shootTimer = 0f;

    void Update()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            peaAnim.SetBool("isAttack", true);
            shootTimer = 0f;
        }
    }

    void Shoot()
    {
        GameObject bullet = GetPooledBullet();
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.SetActive(true);
        StartCoroutine(DisableAttack());
    }

    IEnumerator DisableAttack()
    {
        yield return new WaitForSeconds(0.5f);
        peaAnim.SetBool("isAttack", false);
    }

    GameObject GetPooledBullet()
    {
        // Tìm bullet chưa dùng trong holder
        foreach (Transform child in bulletHolder)
        {
            if (!child.gameObject.activeInHierarchy)
            {
                return child.gameObject;
            }
        }

        // Nếu không có sẵn, tạo mới
        GameObject newBullet = Instantiate(peaBulletPrefab, bulletHolder);
        newBullet.SetActive(false); // Tạm thời tắt để tránh nháy
        bulletPool.Add(newBullet);
        return newBullet;
    }
}
