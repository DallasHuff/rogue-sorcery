using UnityEngine;
using Rogue.Combat.Damage;

public class Projectile : MonoBehaviour
{
    public float damage, lifeTime, knockbackForce;
    public Element damageType { get; set; }

    // Update is called once per frame
    void Update()
    {
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
        lifeTime -= Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // layer 8 == "Enemy"
        if (other.gameObject.layer == 8)
        {
            //IDamageable damageable = other.gameObject.GetComponentInParent<EnemyStats>();
            //if (damageable != null)
            //{
            //    Vector2 direction = (other.transform.position - transform.position).normalized;
            //    Vector2 knockback = direction * knockbackForce;
            //    damageable.TakeDamage(damage, knockback, damageType);
            //    Destroy(gameObject);
            //}

        }
    }
}
