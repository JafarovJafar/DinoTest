using UnityEngine;

public interface IDamageable
{
    void TakeDamage(Vector3 point, Vector3 direction, float strenght);
}