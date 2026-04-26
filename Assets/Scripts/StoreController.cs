using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class StoreController : MonoBehaviour
{

    public static StoreController instance;

    private void Awake()
    {
        instance = this;
    }



    public float currentMoney = 1000f;
    public Transform stockSpawnPoint, furnitureSpawnPoint;
    public List<FurnitureController> shelvingCases = new List<FurnitureController>();
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIController.instance.UpdateMoney(currentMoney);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            AddMoney(120f);
        }

        if (Keyboard.current.uKey.wasPressedThisFrame)
        {
            if (CheckMoneyAvailable(350f))
            {
                SpendMoney(350f);
            }
        }

        


    }



    public void AddMoney(float amountToAdd)
    {
        currentMoney += amountToAdd;
        UIController.instance.UpdateMoney(currentMoney);
    }


    public void SpendMoney(float amountToSpend)
    {
        currentMoney -= amountToSpend;

        if(currentMoney < 0)
        {
            currentMoney = 0;
        }


        UIController.instance.UpdateMoney(currentMoney);
    }

    public bool CheckMoneyAvailable(float amountToCheck)
    {
        bool hasEough = false;

        if(currentMoney >= amountToCheck)
        {
            hasEough = true;
        }

        return hasEough;
    }


   
}
