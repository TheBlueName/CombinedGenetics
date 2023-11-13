using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewInventory : MonoBehaviour
{
    public int woodAmount;

    public int currencyAmount;

    public int spearAmount;

    public int medkitAmount;

    public float craftingTime = 3;

    [Header("Texts")]

    public TextMeshProUGUI woodAmountText;

    public TextMeshProUGUI currencyAmountText;

    public TextMeshProUGUI medkitamountText;

    public GameObject Inventory;

    public bool canCraft;

    public bool canCraftNow;

    void Start()
    {
        canCraftNow = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            woodAmount++;
        }

        if (woodAmount > 2)
        {
            canCraft = true;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Toggle();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            currencyAmount++;
        }

        //UI numbers
        woodAmountText.text = woodAmount.ToString();

        currencyAmountText.text = currencyAmount.ToString();

        medkitamountText.text = medkitAmount.ToString();

        if(Input.GetKeyDown(KeyCode.Q) && medkitAmount > 0)
        {
           gameObject.GetComponent<HealthScript>().DamagePlayer(-20);
           medkitAmount--;
           Debug.Log("Used Medkit");
        }
    }

    private void Toggle()
    {
        bool currentState = Inventory.activeSelf;
        Inventory.SetActive(!currentState);
    }

    public IEnumerator Crafting()
    {
        yield return new WaitForSeconds(craftingTime);
        woodAmount--;
        Debug.Log("Crafting is done!");
    }

    public void AddLogs(int woodGiveAmount)
    {
        woodAmount =+ woodGiveAmount;
    }
}
