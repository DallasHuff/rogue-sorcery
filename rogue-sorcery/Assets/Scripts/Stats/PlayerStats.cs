using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] private FloatReference maxHealth;          // modified by flat amount
    [SerializeField] private FloatReference currentHealth;
    [SerializeField] private FloatReference abilityHaste;       // modified by flat amount
    [SerializeField] private FloatReference armor;              // modified by flat amount
    [SerializeField] private FloatReference damage;             // modified by %
    [SerializeField] private FloatReference speed;              // modified by %
    [SerializeField] private FloatReference projectileSpeed;    // modified by %
    [SerializeField] private FloatReference waterDamage;        // modified by %
    [SerializeField] private FloatReference fireDamage;         // modified by %
    [SerializeField] private FloatReference rockDamage;         // modified by %
    [SerializeField] private FloatReference airDamage;          // modified by %
    [SerializeField] private FloatReference lightningDamage;    // modified by %

    private Rigidbody2D _body;


    /*
     * possible additional stats:
     * critical strike chance
     * luck (increase gold gained, rarity of items from shop)
     * intelligence (increases number of spells you can hold)
    */

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            TakeDamage(10, Element.FIRE);
        }
    }

    private void Die()
    {
        //TODO: Do something when player dies
        Debug.Log("player died");
    }

    public void TakeDamage(float damage, Vector2 knockback, Element damageType)
    {
        damage = Mathf.Clamp(damage, 0, float.MaxValue);
        currentHealth.Variable.Value -= damage;

        _body.AddForce(knockback, ForceMode2D.Impulse);

        if (currentHealth.Value <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage, Element damageType)
    {
        damage = Mathf.Clamp(damage, 0, float.MaxValue);
        currentHealth.Variable.Value -= damage;

        if (currentHealth.Value <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        damage = Mathf.Clamp(damage, 0, float.MaxValue);
        currentHealth.Variable.Value -= damage;

        if (currentHealth.Value <= 0)
        {
            Die();
        }
    }
}