using Unity.VisualScripting;
using UnityEngine;

public class ShieldCollide : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Projectile") || col.CompareTag("EnergyCapsule"))
        {
            col.GetComponent<ProjectileScript>().DestroyProjectile();
        }
    }
}
