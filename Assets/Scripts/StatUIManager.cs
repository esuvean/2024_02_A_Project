using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUIManager : MonoBehaviour
{
    public static StatUIManager Instance {  get; private set; }

    [Header("UI References")]
    public Slider hungerSlider;
    public Slider suitDurabilitySlider;
    public TextMeshProUGUI hungerText;
    public TextMeshProUGUI durabilityText;

    private SurvivalStats survivalStats;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        survivalStats = FindAnyObjectByType<SurvivalStats>();
        hungerSlider.maxValue = survivalStats.maxHunger;
        suitDurabilitySlider.maxValue = survivalStats.maxSuitDurability;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateStatUI()
    {
        hungerSlider.value = survivalStats.currenHunger;
        suitDurabilitySlider.value = survivalStats.currentSuitDurability;

        hungerText.text = $"허기 : {survivalStats.GetHungerPercentage():F0}%";
        durabilityText.text = $"우주복 : {survivalStats.GetSuitDurabilityPercentage():F0}%";

        hungerSlider.fillRect.GetComponent<Image>().color = survivalStats.currenHunger < survivalStats.maxHunger * 0.3f ? Color.red : Color.green;
        suitDurabilitySlider.fillRect.GetComponent<Image>().color = survivalStats.currentSuitDurability < survivalStats.maxSuitDurability * 0.3f ? Color.red : Color.blue;
    }
}
