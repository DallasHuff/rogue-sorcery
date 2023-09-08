using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    public float damage;
    public float lifeTime = 3f;
    private Element damageType = Element.FIRE;

    void Update()
    {
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
        lifeTime -= Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // layer 8 == "Enemy"
        if (collision.gameObject.layer == 8)
        {
            EnemyStats eS = collision.gameObject.GetComponentInParent<EnemyStats>();
            eS.TakeDamage(damage, damageType);
            Destroy(gameObject);
        }
    }
}
