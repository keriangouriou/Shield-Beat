using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    private float currentEnergy = 100;
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
}
