using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f; // �Ѿ� �ӵ�
    public int damageAmount = 20; // �Ѿ� ������
    public float lifeTime = 5f;

    void Start()
    {
        // �Ѿ� �߻� ����� �ӵ� ����
        Rigidbody2D bulletRb = GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.velocity = transform.up * bulletSpeed;
        }
        Destroy(gameObject, lifeTime);
            
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")) // ���� ����� ��
        {
            Destroy(gameObject); // �Ѿ� �ı�
        }
    }
}
