using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TankController : MonoBehaviour
{

    public Rigidbody2D rb2d;
    private Vector2 movementVector;
    public float maxSpeed = 10;
    public float rotationSpeed = 100;
    public float turretRotationSpeed = 150;
    public Transform turretParent;
    public int maxHealth = 100;
    private int currentHealth;
    public string targetSceneName;

    void Start()
    {
        currentHealth = maxHealth;
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
        Debug.Log("Tank has died!");
        SceneManager.LoadScene(targetSceneName);

    }
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void HandleMoveBody(Vector2 movementVector)
    {
        this.movementVector = movementVector;
    }
    public void HandleTurretMovement(Vector2 pointerPosition)
    {
        var turretDirection = (Vector3)pointerPosition - transform.position;
        var desiredAngle = Mathf.Atan2(turretDirection.y, turretDirection.x) * Mathf.Rad2Deg;
        var rotationStep = turretRotationSpeed * Time.deltaTime;
        turretParent.rotation = Quaternion.RotateTowards(turretParent.rotation, Quaternion.Euler(0, 0, desiredAngle-90), rotationStep);
    }


    private void FixedUpdate()
    {
        rb2d.velocity = (Vector2)transform.up * movementVector.y * maxSpeed * Time.deltaTime;
        rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x * rotationSpeed * Time.deltaTime));
    }

    private bool hasKey = false; // 키를 가졌는지 여부를 저장하는 변수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key")) // 충돌한 것이 Key 태그인지 확인
        {
            hasKey = true; // 키를 가지게 됨
            Destroy(collision.gameObject); // 키를 획득하면 해당 키를 파괴
        }

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
        if (collision.gameObject.CompareTag("Door"))
        {
            if (hasKey) // 키를 가지고 있는지 확인
            {
                collision.gameObject.SetActive(false);
                Debug.Log("Door opened!"); // 예시로 디버그 로그 출력

            }
            else
            {
                Debug.Log("You need a key to open this door!"); // 키가 없을 때 메시지 출력
            }
        }
    }


}