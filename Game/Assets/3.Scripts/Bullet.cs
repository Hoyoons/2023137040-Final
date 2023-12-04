using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f; // ÃÑ¾Ë ¼Óµµ
    public int damageAmount = 20; // ÃÑ¾Ë µ¥¹ÌÁö
    public float lifeTime = 5f;

    void Start()
    {
        // ÃÑ¾Ë ¹ß»ç ¹æÇâ°ú ¼Óµµ ¼³Á¤
        Rigidbody2D bulletRb = GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.velocity = transform.up * bulletSpeed;
        }
        Destroy(gameObject, lifeTime);
            
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")) // º®¿¡ ´ê¾ÒÀ» ¶§
        {
            Destroy(gameObject); // ÃÑ¾Ë ÆÄ±«
        }
        if (collision.gameObject.CompareTag("Door")) // º®¿¡ ´ê¾ÒÀ» ¶§
        {
            Destroy(gameObject); // ÃÑ¾Ë ÆÄ±«
        }
    }
}
