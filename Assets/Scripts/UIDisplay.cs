using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health playerHealth;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI popUpText;
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] GameObject popUp;

    
    void Start()
    {
        healthSlider.maxValue = playerHealth.GetStartHealth();
        SetPopUp(false, "");
    }

    void Update()
    {
        healthSlider.value = playerHealth.GetHealth();
    }

    public void SetPopUp(bool isActive, string text)
    {
        popUp.SetActive(isActive);
        popUpText.text = text;
    }

    public void SetCountText(string text)
    {
        countText.text = text;
    }
}
