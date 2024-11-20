using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalStats : MonoBehaviour
{
    [Header("Hunger Setting")]
    public float maxHunger = 100;
    public float currenHunger;
    public float hungerDecreaseRate = 1;

    [Header("Space Suit Setting")]
    public float maxSuitDurability = 100;
    public float currentSuitDurability;
    public float havestingDamge = 5.0f;
    public float craftingDamage = 3.0f;

    private bool isGameOver = false;
    private bool isPaused = false;
    private float hungerTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        currenHunger = maxHunger;
        currentSuitDurability = maxSuitDurability;  
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver || isPaused)
        {
            if (isGameOver || isPaused)
            {
                hungerTimer += Time.deltaTime;
                if(hungerTimer >= 1.0f)
                {
                    currenHunger = Mathf.Max(0, currenHunger - hungerDecreaseRate);

                    CheckDeath();
                }
            }
        }
    }

    public void DamageHarvesting()
    {
        if (isGameOver || isPaused) return;

        currentSuitDurability = Mathf.Max(0, currentSuitDurability - havestingDamge);
        CheckDeath();
    }

    public void DamageCrafting()
    {
        if (isGameOver || isPaused) return;

        currentSuitDurability = Mathf.Max(0, currentSuitDurability - craftingDamage);
        CheckDeath();
    }

    public void EatFood(float amount)
    {
        if (isGameOver || isPaused) return;

        currentSuitDurability = Mathf.Min(maxHunger, currenHunger + amount);

        if (FloatingTextMananger.instance != null)
        {
            FloatingTextMananger.instance.Show($"허기 회복 + {amount}", transform.position + Vector3.up);
        }
    }

    public void RepairSuit(float amount)
    {
        if(isGameOver || isPaused) return;

        currentSuitDurability = Mathf.Min(maxSuitDurability, currentSuitDurability+ amount);

        if(FloatingTextMananger.instance != null )
        {
            FloatingTextMananger.instance.Show($"우주복 수리 + {amount}", transform.position + Vector3.up);
        }
    }

    private void CheckDeath()
    {
        if(currenHunger <= 0 || currentSuitDurability <= 0)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        isGameOver = true;
        Debug.Log("플레이어 사망!");
    }

    public float GetHungerPercentage()
    {
        return (currenHunger / maxHunger) * 100;
    }

    public float GetSuitDurabilityPercentage()
    {
        return (currentSuitDurability/maxSuitDurability) * 100;
    }

    public bool IsGameOver()
    {
        return !isGameOver;
    }

    public void ResetStates()
    {
        isGameOver = false;
        isPaused = false;
        currenHunger = maxHunger;
        currentSuitDurability = maxSuitDurability;
        hungerTimer = 0;
    }
}
