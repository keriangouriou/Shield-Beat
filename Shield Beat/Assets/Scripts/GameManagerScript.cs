using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    private float currentEnergy = 90;
    [SerializeField]
    private Material materialEnergy, materialEnemy;
    private int energyState = 5;
    private Color materialEnergyColor = new Color(0,1,0);
    private Color materialEnemyColor = new Color(1,0,1);
    private float intensity;
    void Start()
    {
        
    }

    void Update()
    {
        currentEnergy -= 10 * Time.deltaTime;
        if (currentEnergy <= 0 )
        {
            Lose();
        }

        switch (currentEnergy)
        {
            case <50:
                EnergyVeryLow();
                break;
            case < 100:
                EnergyLow();
                break;
            case < 150:
                EnergyHigh();
                break;
            case >= 150:
                EnergyVeryHigh();
                break;
        }
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
            intensity = -2;
            materialEnergy.SetColor("_EmissionColor", materialEnergyColor*0f);
            energyState = 1;
        }
    }
    private void EnergyLow()
    {
        if (energyState != 2)
        {
            materialEnergy.SetColor("_EmissionColor", materialEnergyColor*0.2f);
            energyState = 2;
        }
    }
    private void EnergyHigh()
    {
        if (energyState != 3)
        {
            materialEnergy.SetColor("_EmissionColor", materialEnergyColor*0.7f);
            energyState = 3;
        }
    }
    private void EnergyVeryHigh()
    {
        if (energyState != 4)
        {
            materialEnergy.SetColor("_EmissionColor", materialEnergyColor * 1.2f);
            energyState = 4;
        }
    }
}
