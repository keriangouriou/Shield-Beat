using Unity.VisualScripting;
using UnityEngine;

public class ShieldCollide : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Projectile"))
        {
            col.GetComponent<ProjectileScript>().DestroyProjectile();
        }
    }
}
