using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f; // �Ѿ� �ӵ�
    public int damageAmount = 10; // �Ѿ� ������
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
        // �浹�� ������Ʈ�� ������ �±׸� ������ �ִ��� Ȯ��
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject); // �ڽ��� ������Ʈ �ı�
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject); // �ڽ��� ������Ʈ �ı�
        }
    }
}
