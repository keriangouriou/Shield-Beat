using TMPro;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    private float currentEnergy = 90;
    [SerializeField]
    private Material materialEnergy, materialEnemy;
    [SerializeField]
    private TextMeshProUGUI scoreTMP, multiplierTMP;
    [SerializeField]
    private Conductor conductor;
    //Material
    private int energyState = 5;
    private Color materialEnergyColor = new Color(0,1,0);
    private Color materialEnemyColor = new Color(1,0,1);
    //score
    private float points = 0;
    private float multiplier = 1;
    void Start()
    {
    }

    void Update()
    {
        currentEnergy += 10 * Time.deltaTime;
        if (currentEnergy > 400 )
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

        //score and UI
        RefreshUI();
    }
    public void GainEnergy(float energyAdded)
    {
        currentEnergy += energyAdded;
    }
    public void LoseEnergy(float energyLost)
    {
        currentEnergy -= energyLost;
    }
    private void Lose()
    {
        Debug.Log("You Lose");
    }

    private void EnergyVeryLow()
    {
        if (energyState != 1)
        {
            materialEnergy.SetColor("_EmissionColor", materialEnergyColor*0f);
            energyState = 1;
        }
    }
    private void EnergyLow()
    {
        if (energyState != 2)
        {
            materialEnergy.SetColor("_EmissionColor", materialEnergyColor*0.5f);
            energyState = 2;
        }
    }
    private void EnergyMidLow()
    {
        if (energyState != 3)
        {
            materialEnergy.SetColor("_EmissionColor", materialEnergyColor * 0.7f);
            energyState = 3;
        }
    }
    private void EnergyMid()
    {
        if (energyState != 4)
        {
            materialEnergy.SetColor("_EmissionColor", materialEnergyColor * 1f);
            energyState = 4;
        }
    }
    private void EnergyMidHigh()
    {
        if (energyState != 5)
        {
            materialEnergy.SetColor("_EmissionColor", materialEnergyColor * 2f);
            energyState = 5;
        }
    }
    private void EnergyHigh()
    {
        if (energyState != 6)
        {
            materialEnergy.SetColor("_EmissionColor", materialEnergyColor*3f);
            energyState = 6;
        }
    }
    private void EnergyVeryHigh()
    {
        if (energyState != 7)
        {
            materialEnergy.SetColor("_EmissionColor", materialEnergyColor * 4f);
            energyState = 7;
        }
    }
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
    private void RefreshUI()
    {
        scoreTMP.SetText(Mathf.Round(points).ToString());
        multiplierTMP.SetText("x " + multiplier.ToString());
    }
}
