using UnityEngine;

internal interface IKnockbackable
{
    void Knockback(Vector2 knockbackAngle, float knockbackStrength, object facingDirection);
}