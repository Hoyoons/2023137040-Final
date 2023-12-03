using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damageAmount);
                Destroy(collision.gameObject); // ÃÑ¾Ë ÆÄ±«
            }
        }
    }

}