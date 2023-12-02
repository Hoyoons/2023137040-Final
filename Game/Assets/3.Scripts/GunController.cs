using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알 프리팹
    public Transform gunBarrel; // 총구 위치
    public float bulletSpeed = 10f; // 총알 속도

    void Update()
    {
        // 마우스 왼쪽 버튼이나 스페이스 바를 누르면 발사
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        if (bulletRb != null)
        {
            bulletRb.velocity = gunBarrel.up * bulletSpeed; // 총구 방향으로 총알 발사
        }
        else
        {
            Debug.LogError("총알의 Rigidbody2D 컴포넌트를 찾을 수 없습니다.");
        }
    }
}
