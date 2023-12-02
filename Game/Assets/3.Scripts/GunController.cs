using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab; // �Ѿ� ������
    public Transform gunBarrel; // �ѱ� ��ġ
    public float bulletSpeed = 10f; // �Ѿ� �ӵ�

    void Update()
    {
        // ���콺 ���� ��ư�̳� �����̽� �ٸ� ������ �߻�
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
            bulletRb.velocity = gunBarrel.up * bulletSpeed; // �ѱ� �������� �Ѿ� �߻�
        }
        else
        {
            Debug.LogError("�Ѿ��� Rigidbody2D ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }
}
