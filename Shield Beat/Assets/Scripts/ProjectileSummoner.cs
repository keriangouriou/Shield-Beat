using System.Collections.Generic;
using UnityEngine;

public class ProjectileSummoner : MonoBehaviour
{
    [SerializeField]
    private Conductor conductor;
    [SerializeField]
    private GameObject projectileBass, projectilesMelodyOne, projectilesMelodyTwo, projectilesMelodyThree;
    //Rhythm system
    private float crochet;
    private float lastCrochet = 0;
    [SerializeField]
    private bool isPairBeat = true, isFirstPairBeat = true;
    private List<int[]> patternList = new List<int[]>();
    private int currentWave = 0;
    private int pastCrochet = 0;
    //Instantiation info
    private int[] nextPatterns;
    [SerializeField]
    private Vector3 projectileRotation;
    private List<GameObject> projectiles = new List<GameObject>();
    private int rotationDirection = 1;

    private void Start()
    {
        //there is 224 crochet in the music and 224/16 = 14 so there is 14 waves
        patternList.Add(new int[5] { 16, 16, 0, 0, 0 }); //Wave 1 Bass and MelodyOne
        patternList.Add(new int[5] { 16, 0, 16, 0, 16 }); //Wave 2 Bass and MelodyTwo
        patternList.Add(new int[5] { 16, 16, 16, 0, 0 }); // Wave 3->6 Bass, MelodyOne and MelodyTwo
        patternList.Add(new int[5] { 16, 16, 16, 0, 0 });
        patternList.Add(new int[5] { 16, 16, 16, 0, 16 });
        patternList.Add(new int[5] { 16, 16, 16, 0, 16 }); //Wave 6
        patternList.Add(new int[5] { 16, 0, 0, 12, 0 });
        patternList.Add(new int[5] { 16, 0, 0, 12, 16 });
        patternList.Add(new int[5] { 16, 16, 0, 12, 0 });
        patternList.Add(new int[5] { 16, 16, 0, 12, 16 });
        patternList.Add(new int[5] { 16, 16, 0, 12, 0 });
        patternList.Add(new int[5] { 16, 16, 0, 12, 16 });
        patternList.Add(new int[5] { 16, 16, 0, 0, 0 });
        patternList.Add(new int[5] { 16, 16, 0, 0, 16 });
        nextPatterns = new int[5];
        crochet = conductor.crochet;
    }
    private void Update()
    {
        if (conductor.songPosition >= lastCrochet + crochet)
        {
            if (isPairBeat)
            {
                isPairBeat = false;
            }
            else
            {
                isPairBeat = true;
                if (isFirstPairBeat)
                {
                    isFirstPairBeat = false;
                }
                else
                {
                    isFirstPairBeat = true;
                }
            }
           
            SummonNextProjectiles();

            lastCrochet += crochet;

            if (conductor.songPosition > pastCrochet * crochet)
            {
                SendNextWave();
            }
            if (isPairBeat)
            {
                projectileRotation.y += 90 * rotationDirection;
            }
            if (nextPatterns[4] > 0)
            {
                if (rotationDirection != -1)
                {
                    rotationDirection = -1;
                    projectileRotation.z = 180;
                }
            }
            else if (rotationDirection != 1)
            {
                projectileRotation.z = 0;
                rotationDirection = 1;
            }
        }
    }
    private void SummonNextProjectiles()
    {
        Bass();
        MelodyOne();
        MelodyTwo();
        MelodyThree();
    }
    private void Bass()
    {
        if (isPairBeat && nextPatterns[0]>0)
        {
            Instantiate(projectileBass,Vector3.zero, Quaternion.Euler(projectileRotation));
        }
        DecrementPattern(0);
    }
    private void MelodyOne()
    {
        if (isPairBeat && !isFirstPairBeat && nextPatterns[1] > 0)
        {
            Instantiate(projectilesMelodyOne, Vector3.zero, Quaternion.Euler(projectileRotation));
        }
        DecrementPattern(1);
    }
    private void MelodyTwo()
    {
        if (isPairBeat && isFirstPairBeat && nextPatterns[2] > 0)
        {
            Instantiate(projectilesMelodyTwo, Vector3.zero, Quaternion.Euler(projectileRotation));
        }
        DecrementPattern(2);
    }
    private void MelodyThree()
    {
        if (isPairBeat && !isFirstPairBeat && nextPatterns[3] > 0)
        {
            Instantiate(projectilesMelodyThree, Vector3.zero, Quaternion.Euler(projectileRotation));
        }
        DecrementPattern(3);
    }
    private void DecrementPattern(int patternIndex)
    {
        nextPatterns[patternIndex]--;
    }
    private void SendNextWave()
    {
        if (currentWave < 14)
        {
            for (int i = 0; i < nextPatterns.Length; i++)
            {
                nextPatterns[i] = patternList[currentWave][i];
            }
            currentWave++;
            pastCrochet += 16;
        }
    }
    public void StopAllProjectiles()
    {
        foreach (GameObject go in projectiles)
        {
            if (go != null)
            {
                go.GetComponent<ProjectileScript>().StopProjectile();
            }
        }
        this.enabled = false;
    }
    public void RemoveFromProjectiles(GameObject projectileToRemove)
    {
        projectiles.Remove(projectileToRemove);
    }
    public void AddInProjectiles(GameObject projectileToAdd)
    {
        projectiles.Add(projectileToAdd);
    }
}
