using System.Collections;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField]
    private float speed, beat;
    public float value;
    public float pointGain;
    private void Update()
    {
            transform.Translate(0,0,-speed*Time.deltaTime);
    }
    public void DestroyProjectile()
    {
        Destroy(transform.parent.gameObject);
    }
}
