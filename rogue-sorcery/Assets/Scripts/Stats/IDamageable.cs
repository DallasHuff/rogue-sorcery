using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(float damage, Vector2 knockback, Element damageType);
    public void TakeDamage(float damage, Element damageType);
    public void TakeDamage(float damage);

}
