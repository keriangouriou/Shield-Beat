using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    [SerializeField]
    private AudioSource music;
    [SerializeField]
    private ProjectileSummoner projectileSummoner;
    public float crochet;
    private float offSet = 0.25f;
    private float dpsTimeSong;
    public float songPosition;
    void Start()
    {
        StartCoroutine(WaitBeforeStart());
    }

    // Update is called once per frame
    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dpsTimeSong)* music.pitch - offSet  ;
    }

    IEnumerator WaitBeforeStart()
    {
        yield return new WaitForSeconds(2f);
        dpsTimeSong = (float)AudioSettings.dspTime;
        projectileSummoner.enabled = true;
        yield return new WaitForSeconds(3f);
        music.Play();
    }
}
