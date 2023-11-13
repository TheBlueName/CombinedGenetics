using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNeeds : MonoBehaviour
{
    [SerializeField] private float CurrentHunger;
    [SerializeField] private float MaxHunger = 100f;
    [SerializeField] private float HungerDuration = 0.5f;
    public float CurrentThirst;
    [SerializeField] private float MaxThirst = 100f;
    [SerializeField] private float ThirstDuration = 0.5f;
    [SerializeField] private Text HungerText;
    [SerializeField] private TextMeshProUGUI ThirstText;
    [SerializeField] private Slider HungerSlider;
    [SerializeField] private Slider ThirstSlider;
    [SerializeField] private Image HungerImage;
    [SerializeField] private Image ThirstImage;
    [SerializeField] private Color CriticalColor;
    [SerializeField] private Color AlmostCriticalColor;
    [SerializeField] private Color HungerColor;
    [SerializeField] private Color ThirstColor;

    void Start()
    {
        CurrentThirst = MaxThirst;
        CurrentHunger = MaxHunger;
        StartCoroutine(HungerDrain());
        StartCoroutine(ThirstDrain());
    }

    void Update()
    {
        HungerText.text = CurrentHunger.ToString();
        ThirstText.text = CurrentThirst.ToString();
        HungerSlider.value = CurrentHunger;
        ThirstSlider.value = CurrentThirst;

        if (CurrentHunger <= 20) HungerImage.color = AlmostCriticalColor;
        else HungerImage.color = HungerColor;
        if (CurrentHunger <= 10) HungerImage.color = CriticalColor;
        else if (CurrentHunger <= 20) HungerImage.color = AlmostCriticalColor;
        else if (CurrentHunger > 20) HungerImage.color = HungerColor;

        if (CurrentThirst <= 20) ThirstImage.color = AlmostCriticalColor;
        else ThirstImage.color = ThirstColor;
        if (CurrentThirst <= 10) ThirstImage.color = CriticalColor;
        else if (CurrentThirst <= 20) ThirstImage.color = AlmostCriticalColor;
        else if (CurrentThirst > 20) ThirstImage.color = ThirstColor;

        if (CurrentHunger <= 0)
        {
            HealthScript hs = gameObject.GetComponent<HealthScript>();
            hs.Die();
        }

        if(CurrentHunger >= MaxHunger)
        {
            CurrentHunger = MaxHunger;
        }

        if (CurrentThirst <= 0)
        {
            HealthScript hs = gameObject.GetComponent<HealthScript>();
            hs.Die();
        }

        if (CurrentThirst >= MaxThirst)
        {
            CurrentThirst = MaxThirst;
        }
    }

    public void Eat(int Nutrition)
    {
        CurrentHunger += Nutrition;
    }

    IEnumerator HungerDrain()
    {
        while (true)
        {
            yield return new WaitForSeconds(HungerDuration);
            CurrentHunger--;
        }
    }

    IEnumerator ThirstDrain()
    {
        while (true)
        {
            yield return new WaitForSeconds(ThirstDuration);
            CurrentThirst--;
        }
    }

    public void Drink()
    {
        CurrentThirst = MaxThirst;
    }
}
