using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControler : MonoBehaviour
{
    
    public Transform player; // �÷��̾��� Transform
    public Transform turretHead; // ��ž�� �Ӹ�(ȸ�� �κ�)
    public GameObject bulletPrefab; // �Ѿ� ������
    public Transform firePoint; // �߻� ��ġ
    public float fireRate = 1f; // �߻� �ӵ�
    public float detectionRange = 15f; // ���� ����
    public float rotationSpeed = 5f; // ȸ�� �ӵ�
    public int maxHealth = 60;
    private int currentHealth;
    public GameObject newObjectPrefab;

    private float nextFireTime = 0f;
    private bool isPlayerDetected = false;
    private Quaternion initialRotation; // �ʱ� ȸ���� ����� ����

    void Start()
    {
        currentHealth = maxHealth;
        initialRotation = turretHead.rotation; // ������ �� �ʱ� ȸ���� ����
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        gameObject.SetActive(false);
        GameObject newObject = Instantiate(newObjectPrefab, transform.position, transform.rotation);
        newObject.SetActive(true);
        Debug.Log("Turret has died!");

    }

    void Update()
    {

        float distanceOffset = 1.0f; // ���� ��ġ�� �����ϱ� ���� ��
        Vector2 raycastStartPoint = turretHead.position + (turretHead.up * distanceOffset); // �ͷ� �������� distanceOffset��ŭ �̵��� ����

        Vector2 direction = raycastStartPoint; // [�ּ� 1] �� �κ��� ������ �ʿ��մϴ�.
        RaycastHit2D hit = Physics2D.Raycast(raycastStartPoint, direction, detectionRange); // 2D ����ĳ��Ʈ


        FindPlayer();
        if (player != null)
        {
            if (CanSeePlayer())
            {
                isPlayerDetected = true;
            }
            else
            {
                isPlayerDetected = false;
            }

            RotateTurret();
            if (isPlayerDetected && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
        else
        {
            isPlayerDetected = false;
        }

        //if (!isPlayerDetected)
        //{
        //    ReturnToInitialRotation();
        //}


    }

    void FindPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player"); // "Player" �±׸� ���� ������Ʈ �˻�
        if (playerObject != null)
        {
            player = playerObject.transform; // �÷��̾��� Transform ����
        }
    }

    bool CanSeePlayer()
    {
        Vector2 direction = player.position - turretHead.position; // �÷��̾�� ��ž ���� ���� ���� ���
        RaycastHit2D hit = Physics2D.Raycast(turretHead.position, direction, detectionRange); // �÷��̾� ������ 2D ����ĳ��Ʈ

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player")) // �÷��̾�� �浹�ߴ��� Ȯ��
            {
                return true; // �÷��̾ ������
            }
        }
        return false; // �÷��̾� ���� ����
    }

    void RotateTurret()
    {
        if (isPlayerDetected)
        {
            Vector2 direction = player.position - turretHead.position; // �÷��̾� ���� ���
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // �÷��̾� ���������� ���� ���
            float angleDifference = angle - turretHead.rotation.eulerAngles.z; // ���� ������ �÷��̾� ���� ���� ���

            if (Mathf.Abs(angleDifference) > 1f)
            {
                turretHead.rotation = Quaternion.RotateTowards(turretHead.rotation, Quaternion.AngleAxis(angle - 90f, Vector3.forward), rotationSpeed * Time.deltaTime); // ��ž ȸ��
            }
        }
    }

    //void ReturnToInitialRotation()
    //{
    //    // �÷��̾ �������� ���� �� �ʱ� ȸ�������� ȸ��
    //    turretHead.rotation = Quaternion.RotateTowards(turretHead.rotation, initialRotation, rotationSpeed * Time.deltaTime);
    //}

    void Shoot()
    {
        // �Ѿ� �߻�
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damageAmount);
                Destroy(collision.gameObject); // �Ѿ� �ı�
            }
        }
    }
}
