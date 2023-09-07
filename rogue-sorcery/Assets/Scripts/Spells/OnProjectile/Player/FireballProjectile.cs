using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    private float lifeTime = 3f;
    private float damage;
    private Element damageType = Element.FIRE;

    // Update is called once per frame
    void Update()
    {
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
        lifeTime -= Time.deltaTime;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //EnemyStatBehavior eSB = collision.gameObject.GetComponentInParent<EnemyStatBehavior>();
            //eSB.TakeDamage(damage, damageType);
            Destroy(gameObject);
        }
    }
}
