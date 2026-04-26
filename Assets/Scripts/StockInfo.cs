using UnityEngine;

[System.Serializable]
public class StockInfo
{
    public string name;
    public enum StockType
    {
        cereal, bigDrink, chipsTube, fruit, fruitlarge

    };
    public StockType typeOfStock;

    public float price, currentPrice;


    public StockObject stockobject;

}
