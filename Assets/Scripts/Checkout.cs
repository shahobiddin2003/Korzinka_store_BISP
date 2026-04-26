using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CheckOut : MonoBehaviour
{

    public static CheckOut instance;

    private void Awake () { instance = this; }


    public TMP_Text priceText;
    public GameObject CheckoutScreen;
    public Transform queuePoint;
    public List<Customer> customersInQueue = new List<Customer>();





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ShowPrice(12.25f);
        HidePrice();
    }

    // Update is called once per frame
    void Update()
    {
        if(customersInQueue.Count > 0 && CheckoutScreen.activeSelf == false)
        {
            if (Vector3.Distance(customersInQueue[0].transform.position, queuePoint.position) < .1f)
            {
                ShowPrice(customersInQueue[0].GetTotalSpend());
            } 
        }
    }

    public void ShowPrice(float priceTotal)
    {
        CheckoutScreen.SetActive(true);
        priceText.text = "$" + priceTotal.ToString("F2");
    }
    public void HidePrice()
    {
        CheckoutScreen.SetActive(false);
    }

    public void CheckOutCustomer()
    {   

        if(CheckoutScreen.activeSelf == true && customersInQueue.Count > 0)
        {
            HidePrice();
            StoreController.instance.AddMoney(customersInQueue[0].GetTotalSpend());

            customersInQueue[0].StartLeaving();
            customersInQueue.RemoveAt(0);
            UpdateQueue();
        }
        else
        {

        }
    }

    public void AddCustomerToQueue(Customer newCust)
    {
        customersInQueue.Add(newCust);
        UpdateQueue();
    }

    public void UpdateQueue()
    {
        for (int i = 0; i < customersInQueue.Count; i++)
        {
            customersInQueue[i].UpdateQueuePoint(queuePoint.position + (queuePoint.forward * i * .6f));
        }
    }

}
