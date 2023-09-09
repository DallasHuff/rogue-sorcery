using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlacialSpike", menuName = "Spells/GlacialSpike")]
public class GlacialSpikeSpell : Spell
{
    [SerializeField] private GameObject GlacialSpikeGO;
    [SerializeField] private float damage;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private FloatReference damageStat;
    [SerializeField] private FloatReference waterDamageStat;
    [SerializeField] private FloatReference projectileSpeedStat;

    public override AbilityState Cast(Transform playerTrans)
    {
        GameObject GlacialSpike = Instantiate(GlacialSpikeGO, playerTrans.position + playerTrans.right, Quaternion.identity);
        // looking left
        if (playerTrans.localScale.x < 0)
        {
            GlacialSpike.GetComponent<Rigidbody2D>().velocity = playerTrans.right * projectileSpeed * -1f;
        }
        // looking right
        else
        {
            GlacialSpike.GetComponent<Rigidbody2D>().velocity = playerTrans.right * projectileSpeed;
        }
        GlacialSpike.GetComponent<GlacialSpikeProjectile>().damage = 10;
        return AbilityState.READY;
    }
}