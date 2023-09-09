using UnityEngine;

public class GlacialSpikeProjectile : Projectile
{
    private void Awake()
    {
        damageType = Element.WATER;
        lifeTime = 3f;
    }
}
