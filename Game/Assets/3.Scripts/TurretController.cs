using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControler : MonoBehaviour
{
    
    public Transform player; // 플레이어의 Transform
    public Transform turretHead; // 포탑의 머리(회전 부분)
    public GameObject bulletPrefab; // 총알 프리팹
    public Transform firePoint; // 발사 위치
    public float fireRate = 1f; // 발사 속도
    public float detectionRange = 15f; // 감지 범위
    public float rotationSpeed = 5f; // 회전 속도
    public int maxHealth = 60;
    private int currentHealth;
    public GameObject newObjectPrefab;

    private float nextFireTime = 0f;
    private bool isPlayerDetected = false;
    private Quaternion initialRotation; // 초기 회전값 저장용 변수

    void Start()
    {
        currentHealth = maxHealth;
        initialRotation = turretHead.rotation; // 시작할 때 초기 회전값 저장
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

        float distanceOffset = 1.0f; // 시작 위치를 조정하기 위한 값
        Vector2 raycastStartPoint = turretHead.position + (turretHead.up * distanceOffset); // 터렛 방향으로 distanceOffset만큼 이동한 지점

        Vector2 direction = raycastStartPoint; // [주석 1] 이 부분은 수정이 필요합니다.
        RaycastHit2D hit = Physics2D.Raycast(raycastStartPoint, direction, detectionRange); // 2D 레이캐스트


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
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player"); // "Player" 태그를 가진 오브젝트 검색
        if (playerObject != null)
        {
            player = playerObject.transform; // 플레이어의 Transform 설정
        }
    }

    bool CanSeePlayer()
    {
        Vector2 direction = player.position - turretHead.position; // 플레이어와 포탑 간의 방향 벡터 계산
        RaycastHit2D hit = Physics2D.Raycast(turretHead.position, direction, detectionRange); // 플레이어 감지용 2D 레이캐스트

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player")) // 플레이어와 충돌했는지 확인
            {
                return true; // 플레이어를 감지함
            }
        }
        return false; // 플레이어 감지 실패
    }

    void RotateTurret()
    {
        if (isPlayerDetected)
        {
            Vector2 direction = player.position - turretHead.position; // 플레이어 방향 계산
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 플레이어 방향으로의 각도 계산
            float angleDifference = angle - turretHead.rotation.eulerAngles.z; // 현재 각도와 플레이어 각도 차이 계산

            if (Mathf.Abs(angleDifference) > 1f)
            {
                turretHead.rotation = Quaternion.RotateTowards(turretHead.rotation, Quaternion.AngleAxis(angle - 90f, Vector3.forward), rotationSpeed * Time.deltaTime); // 포탑 회전
            }
        }
    }

    //void ReturnToInitialRotation()
    //{
    //    // 플레이어를 감지하지 않을 때 초기 회전값으로 회전
    //    turretHead.rotation = Quaternion.RotateTowards(turretHead.rotation, initialRotation, rotationSpeed * Time.deltaTime);
    //}

    void Shoot()
    {
        // 총알 발사
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
                Destroy(collision.gameObject); // 총알 파괴
            }
        }
    }
}
