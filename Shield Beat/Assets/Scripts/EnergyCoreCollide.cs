using UnityEngine;

public class EnergyCoreCollide : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Projectile"))
        {
            col.GetComponent<ProjectileScript>().DestroyProjectile();
        }
    }
}
