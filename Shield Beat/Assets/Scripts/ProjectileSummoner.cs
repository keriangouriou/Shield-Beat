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
    private int patternRotation = 0;

    private void Start()
    {
        //there is 224 crochet in the music and 224/16 = 14 so there is 14 waves
        patternList.Add(new int[5] { 16, 16, 0, 0, 0 }); //Wave 1 Bass and MelodyOne
        patternList.Add(new int[5] { 16, 0, 16, 0, 0 }); //Wave 2 Bass and MelodyTwo
        patternList.Add(new int[5] { 16, 16, 16, 0, 0 }); // Wave 3->6 Bass, MelodyOne and MelodyTwo
        patternList.Add(new int[5] { 16, 16, 16, 0, 0 });
        patternList.Add(new int[5] { 16, 16, 16, 0, 0 });
        patternList.Add(new int[5] { 16, 16, 16, 0, 0 }); //Wave 6
        patternList.Add(new int[5] { 16, 0, 0, 16, 0 });
        patternList.Add(new int[5] { 16, 0, 0, 16, 0 });
        patternList.Add(new int[5] { 16, 16, 0, 16, 0 });
        patternList.Add(new int[5] { 16, 16, 0, 16, 0 });
        patternList.Add(new int[5] { 16, 16, 0, 16, 0 });
        patternList.Add(new int[5] { 16, 16, 0, 16, 0 });
        patternList.Add(new int[5] { 16, 16, 0, 0, 0 });
        patternList.Add(new int[5] { 16, 16, 0, 0, 0 });
        nextPatterns = new int[5];
        nextPatterns[0] = 0; 
        nextPatterns[1] = 0;
        nextPatterns[2] = 0;
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

            if (conductor.songPosition > pastCrochet*crochet)
            {
                SendNextWave();
            }
            if (isPairBeat) 
            {
                patternRotation += 90;
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
            Instantiate(projectileBass,Vector3.zero,Quaternion.Euler(0,patternRotation,0));
        }
        DecrementPattern(0);
    }
    private void MelodyOne()
    {
        if (isPairBeat && !isFirstPairBeat && nextPatterns[1] > 0)
        {
            Instantiate(projectilesMelodyOne, Vector3.zero, Quaternion.Euler(0, patternRotation, 0));
        }
        DecrementPattern(1);
    }
    private void MelodyTwo()
    {
        if (isPairBeat && isFirstPairBeat && nextPatterns[2] > 0)
        {
            Instantiate(projectilesMelodyTwo, Vector3.zero, Quaternion.Euler(0, patternRotation, 0));
        }
        DecrementPattern(2);
    }
    private void MelodyThree()
    {
        if (isPairBeat && !isFirstPairBeat && nextPatterns[3] > 0)
        {
            Instantiate(projectilesMelodyThree, Vector3.zero, Quaternion.Euler(0, patternRotation, 0));
        }
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
}
