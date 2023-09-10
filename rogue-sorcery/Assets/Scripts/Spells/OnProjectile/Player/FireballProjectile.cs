using UnityEngine;

public class FireballProjectile : Projectile
{
    private void Awake()
    {
        damageType = Element.FIRE;
        lifeTime = 3f;
    }
}

// delainey was here
