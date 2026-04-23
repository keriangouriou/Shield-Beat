using System.Collections;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField]
    private float speed, beat;
    private float beatDistance = 9.2308f; //The result of Time in second between beats * speed
    public float value;
    private void Start()
    {
        transform.Translate(0,0, beatDistance * (beat-1));
    }
    private void Update()
    {
            transform.Translate(0,0,-speed*Time.deltaTime);
    }
    public void DestroyProjectile()
    {
        Destroy(transform.parent.gameObject);
    }
}
