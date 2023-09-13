using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyHealthbar healthbar;
    [SerializeField] private FloatVariable maxHealth, damage, armor, speed, projectileSpeed;
    private Rigidbody2D _body;

    private float currentHealth;
    private float dam;
    private float arm;
    private float spd;
    private float projectileSpd;

    void Awake()
    {
        currentHealth = maxHealth.Value;
        dam = damage.Value;
        arm = armor.Value;
        spd = speed.Value;
        projectileSpd = projectileSpeed.Value;

        _body = GetComponent<Rigidbody2D>();

        healthbar.SetHealth(currentHealth, maxHealth.Value);
    }

    void Die()
    {
        //TODO: make dying animation, and fade out
        Destroy(gameObject);
    }

    public void TakeDamage(float damage, Vector2 knockback, Element damageType)
    {
        damage = Mathf.Clamp(damage, 0, float.MaxValue);
        currentHealth -= damage;

        healthbar.SetHealth(currentHealth, maxHealth.Value);

        _body.AddForce(knockback, ForceMode2D.Impulse);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage, Element damageType)
    {
        damage = Mathf.Clamp(damage, 0, float.MaxValue);
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        damage = Mathf.Clamp(damage, 0, float.MaxValue);
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
}