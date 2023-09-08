using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    //public Director d;
    [SerializeField] private FloatVariable maxHealth, damage, armor, speed, projectileSpeed;
    private float currHealth;
    private float dam;
    private float arm;
    private float spd;
    private float projectileSpd;

    void Awake()
    {
        currHealth = maxHealth.Value;
        dam = damage.Value;
        arm = armor.Value;
        spd = speed.Value;
        projectileSpd = projectileSpeed.Value;
    }

    public void TakeDamage(float damage, Element damageType)
    {
        // clamp damage
        damage = Mathf.Clamp(damage, 0, float.MaxValue);
        // armor calculation
        damage *= 1 - arm / (100 + arm);
        currHealth -= damage;
        if (currHealth < 0)
        {
            Die();
        }

    }

    void Die()
    {
        //TODO: make dying animation, and fade out
        Destroy(gameObject);
    }
}