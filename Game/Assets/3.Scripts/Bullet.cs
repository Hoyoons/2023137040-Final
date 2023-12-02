using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f; // 총알 속도
    public int damageAmount = 10; // 총알 데미지
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
        // 충돌한 오브젝트가 적절한 태그를 가지고 있는지 확인
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject); // 자신의 오브젝트 파괴
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject); // 자신의 오브젝트 파괴
        }
    }
}
