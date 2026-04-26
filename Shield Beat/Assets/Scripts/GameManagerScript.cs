using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    private float currentEnergy = 90;
    [SerializeField]
    private Material materialEnergy, materialEnemy;
    [SerializeField]
    private TextMeshProUGUI scoreTMP, multiplierTMP, finalScore;
    [SerializeField]
    private Conductor conductor;
    [SerializeField]
    private ProjectileSummoner projectileSummoner;
    [SerializeField]
    private GameObject lightRing1, lightRing2, lightRing3, lightRing4;
    [SerializeField]
    private Animator blackscreenAnimator;
    [SerializeField]
    private GameObject deathScreen, scoreUI, blackScreen, winScreen;
    [SerializeField]
    private ShieldControlsScript shieldControlsScript;
    //Material
    private int energyState = 5;
    private Color materialEnergyColor = new Color(0,1,0);
    //score
    private float points = 0;
    private float multiplier = 1;
    //GameManager
    private bool timeStop = false;

    private void Start()
    {
        StartCoroutine(DisableBlackScreen());
    }
    void Update()
    {
        if (!timeStop)
        {
            currentEnergy += 10 * Time.deltaTime;
            if (currentEnergy > 400)
            {
                currentEnergy = 400;
            }
            else if (currentEnergy <= 0)
            {
                Lose();
            }

            switch (currentEnergy)
            {
                case < 50:
                    EnergyVeryLow();
                    break;
                case < 100:
                    EnergyLow();
                    break;
                case < 150:
                    EnergyMidLow();
                    break;
                case < 200:
                    EnergyMid();
                    break;
                case < 300:
                    EnergyMidHigh();
                    break;
                case < 350:
                    EnergyHigh();
                    break;
                case >= 350:
                    EnergyVeryHigh();
                    break;
            }
            if (conductor.songPosition >= 108)
            {
                Win();
            }
        }

        //score and UI
        RefreshUI();
    }



        //EnergySystem
    public void GainEnergy(float energyAdded)
    {
        currentEnergy += energyAdded; //This function was for the energy capsule a projectile that gives you energy rather than removing it.
    }                                 //But it isn't part of the game anymore, still I let that there just in case I want to re-implement it later.
    public void LoseEnergy(float energyLost)
    {
        currentEnergy -= energyLost;
    }

        //Game Management Functions
    private void Lose()
    {
        timeStop = true;
        shieldControlsScript.enabled = false;
        projectileSummoner.StopAllProjectiles();
        scoreUI.SetActive(false);
        deathScreen.SetActive(true);
    }
    private void Win()
    {
        winScreen.SetActive(true);
        finalScore.SetText("Final Score:\n"+ Mathf.Round(points).ToString());
    }
    public void ResetGame()
    {
        StartCoroutine(Reset());
    }
    public void QuitGame()
    {
        StartCoroutine(Quit());
    }
    private IEnumerator Reset()
    {
        FadeInBlackScreen();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("LVL1");
    }
    private IEnumerator Quit()
    {
        FadeInBlackScreen();
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
        //Visual Environment functions 
    private void EnergyVeryLow()
    {
        if (energyState != 1)
        {
            materialEnergy.SetColor("_EmissionColor", materialEnergyColor*0f);
            energyState = 1;
            ManageLight(0);
        }
    }
    private void EnergyLow()
    {
        if (energyState != 2)
        {
            materialEnergy.SetColor("_EmissionColor", materialEnergyColor*0.5f);
            energyState = 2;
            ManageLight(1);
        }
    }
    private void EnergyMidLow()
    {
        if (energyState != 3)
        {
            materialEnergy.SetColor("_EmissionColor", materialEnergyColor * 0.7f);
            energyState = 3;
            ManageLight(1);
        }
    }
    private void EnergyMid()
    {
        if (energyState != 4)
        {
            materialEnergy.SetColor("_EmissionColor", materialEnergyColor * 1f);
            energyState = 4;
            ManageLight(2);
        }
    }
    private void EnergyMidHigh()
    {
        if (energyState != 5)
        {
            materialEnergy.SetColor("_EmissionColor", materialEnergyColor * 2f);
            energyState = 5;
            ManageLight(3);
        }
    }
    private void EnergyHigh()
    {
        if (energyState != 6)
        {
            materialEnergy.SetColor("_EmissionColor", materialEnergyColor*3f);
            energyState = 6;
            ManageLight(3);
        }
    }
    private void EnergyVeryHigh()
    {
        if (energyState != 7)
        {
            materialEnergy.SetColor("_EmissionColor", materialEnergyColor * 4f);
            energyState = 7;
            ManageLight(4);
        }
    }
    private void ManageLight(int availableLights)
    {
        switch (availableLights)
        {
            case 0:
                lightRing1.SetActive(false);
                lightRing2.SetActive(false);
                lightRing3.SetActive(false);
                lightRing4.SetActive(false);
                break;
            case 1:
                lightRing1.SetActive(true);
                lightRing2.SetActive(false);
                lightRing3.SetActive(false);
                lightRing4.SetActive(false);
                break;
            case 2:
                lightRing1.SetActive(true);
                lightRing2.SetActive(true);
                lightRing3.SetActive(false);
                lightRing4.SetActive(false);
                break;
            case 3:
                lightRing1.SetActive(true);
                lightRing2.SetActive(true);
                lightRing3.SetActive(true);
                lightRing4.SetActive(false);
                break;
            case 4:
                lightRing1.SetActive(true);
                lightRing2.SetActive(true);
                lightRing3.SetActive(true);
                lightRing4.SetActive(true);
                break;
        }
    }

        // Score Functions
    public void GainPoint(float pointGain)
    {
        points += pointGain*multiplier;
        multiplier += 0.01f;
        multiplier = Mathf.Round(multiplier * 100) / 100;
    }
    public void ResetMultiplier()
    {
        multiplier = 1;
    }
        //UI Functions
    private void RefreshUI()
    {
        scoreTMP.SetText(Mathf.Round(points).ToString());
        multiplierTMP.SetText("x " + multiplier.ToString());
    }
    private void FadeInBlackScreen()
    {
        blackScreen.SetActive(true);
        blackscreenAnimator.Play("BlackScreenFadeIn");
    }
    private IEnumerator DisableBlackScreen()
    {
        yield return new WaitForSeconds(1f);
        blackScreen.SetActive(false);
    }
    
}
