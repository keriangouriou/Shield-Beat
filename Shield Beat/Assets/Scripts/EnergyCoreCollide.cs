using UnityEngine;

public class EnergyCoreCollide : MonoBehaviour
{
    [SerializeField]
    private GameManagerScript gameManager;
    private void OnTriggerEnter(Collider col)
    {
        if ( col.CompareTag("EnergyCapsule"))
        {
            ProjectileScript projectileScript = col.GetComponent<ProjectileScript>();
            gameManager.GainEnergy(projectileScript.value);
            projectileScript.DestroyProjectile();
        }
        else if (col.CompareTag("Projectile"))
        {
            ProjectileScript projectileScript = col.GetComponent<ProjectileScript>();
            gameManager.LoseEnergy(projectileScript.value);
            gameManager.ResetMultiplier();
            projectileScript.DestroyProjectile();
        }
    }
}
