using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f; // 총알 속도
    public int damageAmount = 20; // 총알 데미지
    public float lifeTime = 5f;

    void Start()
    {
        // 총알 발사 방향과 속도 설정
        Rigidbody2D bulletRb = GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.velocity = transform.up * bulletSpeed;
        }
        Destroy(gameObject, lifeTime);
            
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")) // 벽에 닿았을 때
        {
            Destroy(gameObject); // 총알 파괴
        }
    }
}
