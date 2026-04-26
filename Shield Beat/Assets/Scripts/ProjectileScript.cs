using System.Collections;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField]
    private float speed, beat;
    [SerializeField]
    private ProjectileSummoner projectileSummoner;
    public float value;
    public float pointGain;
    private void Awake()
    {
        projectileSummoner = GameObject.Find("GameManager").GetComponent<ProjectileSummoner>();
        projectileSummoner.AddInProjectiles(this.gameObject);
    }
    private void Update()
    {
            transform.Translate(0,0,-speed*Time.deltaTime);
    }
    public void DestroyProjectile()
    {
        projectileSummoner.RemoveFromProjectiles(this.gameObject);
        Destroy(transform.parent.gameObject);
    }
    public void StopProjectile()
    {
        speed = 0;
    }
}
