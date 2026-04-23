using Unity.VisualScripting;
using UnityEngine;

public class ShieldCollide : MonoBehaviour
{
    [SerializeField]
    GameManagerScript gameManagerScript;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Projectile") || col.CompareTag("EnergyCapsule"))
        {
            audioSource.Play();
            ProjectileScript projectileScript = col.gameObject.GetComponent<ProjectileScript>();
            gameManagerScript.GainPoint(projectileScript.pointGain);
            projectileScript.DestroyProjectile();
        }
    }
}
