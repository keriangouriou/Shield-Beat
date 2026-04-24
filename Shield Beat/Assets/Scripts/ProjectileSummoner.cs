using System.Collections;
using UnityEngine;

public class ProjectileSummoner : MonoBehaviour
{
    [SerializeField]
    private Conductor conductor;
    [SerializeField]
    private GameObject projectile;
    //Rhythm system
    private float crochet;
    private float lastBass = 0;
    //Instantiation info
    private int bassRotation = 0;

    private void Start()
    {
        crochet = conductor.crochet;
    }
    private void Update()
    {
        //BassProjectiles
        if (conductor.songPosition > lastBass + crochet*2)
        {
            Instantiate(projectile, Vector3.zero, Quaternion.Euler(0,bassRotation,0));
            bassRotation += 90;
            lastBass += crochet*2;
        }
    }
}
