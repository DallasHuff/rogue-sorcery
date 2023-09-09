using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fireball", menuName = "Spells/Fireball")]
public class FireballSpell : Spell
{
    [SerializeField] private GameObject fireballGO;
    [SerializeField] private float damage;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private FloatReference damageStat;
    [SerializeField] private FloatReference fireDamageStat;
    [SerializeField] private FloatReference projectileSpeedStat;

    public override AbilityState Cast(Transform playerTrans)
    {
        GameObject fireball = Instantiate(fireballGO, playerTrans.position, Quaternion.identity);
        // looking left
        if (playerTrans.localScale.x < 0)
        {
            fireball.GetComponent<Rigidbody2D>().velocity = playerTrans.right * projectileSpeed * -1f;
        }
        // looking right
        else
        {
            fireball.GetComponent<Rigidbody2D>().velocity = playerTrans.right * projectileSpeed;
        }

        FireballProjectile fbpComponent = fireball.GetComponent<FireballProjectile>();
        fbpComponent.damage = damage;
        fbpComponent.knockbackForce = knockbackForce;

        currCD = cooldownTime;
        return AbilityState.COOLDOWN;
    }
}
