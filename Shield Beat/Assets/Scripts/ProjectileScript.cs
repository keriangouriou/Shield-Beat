using System.Collections;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public float value;
    private void Update()
    {
            transform.Translate(0,0,-speed*Time.deltaTime);
    }
    public void DestroyProjectile()
    {
        Destroy(transform.parent.gameObject);
    }
}
