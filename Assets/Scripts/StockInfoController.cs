using UnityEngine;
using System.Collections.Generic;

public class StockInfoController : MonoBehaviour
{

    public List<StockInfo> foodInfo, produceInfo;
    public  List<StockInfo> AllStock = new List<StockInfo> ();

    public static StockInfoController instance;


    private void Awake()
    {

        instance = this;
        AllStock.AddRange(foodInfo);
        AllStock.AddRange(produceInfo);


        for (int i = 0; i < AllStock.Count; i++)
        {
            if (AllStock[i].currentPrice == 0)
            {
                AllStock[i].currentPrice = AllStock[i].price;
            }
        }

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

     
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public StockInfo GetInfo(string stockName)
    {
        StockInfo infoToReturn = null;


        for(int i = 0; i<AllStock.Count; i++)
        {
            if(AllStock[i].name == stockName)
            {
                infoToReturn = AllStock[i];
            }
        }



        return infoToReturn;
    }



    public void UpdatePrice(string stockName, float newPrice)
    {
        for (int i = 0; i < AllStock.Count; i++)
        {
            if (AllStock[i].name == stockName)
            {
                AllStock[i].currentPrice = newPrice;
            }
        }


        List<ShelfSpaceController> shelves = new List<ShelfSpaceController>();

        shelves.AddRange(FindObjectsByType<ShelfSpaceController>( FindObjectsSortMode.None));

        foreach (ShelfSpaceController shelf in shelves)
        {
            if(shelf.info.name == stockName)
            {
                shelf.UpdateDisplayPrice(newPrice);
            }
        }



    }
}
