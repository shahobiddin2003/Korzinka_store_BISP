using System.Collections.Generic;
using TMPro;

using UnityEngine;
public class ShelfSpaceController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public StockInfo info;

    public List<StockObject> objectOnShelf;
    public List<Transform> bigDrinkPoints,cerealPoints,tubeChipsPoints,fruitPoints,LargefruitPoints;
    public TMP_Text shelfLabel;
    

    // public int amountOnShelf;
    public void PlaceStock(StockObject objectToPlace)
    {

        bool preventPlacing = true;
        
        if (objectOnShelf.Count == 0)
        {
            info = objectToPlace.info;
            preventPlacing = false;
        }
        else
        {
            if (info.name == objectToPlace.info.name)
            {
                preventPlacing = false;

                switch (info.typeOfStock)
                {
                    case StockInfo.StockType.bigDrink:


                        if (objectOnShelf.Count >= bigDrinkPoints.Count)
                        {
                            preventPlacing = true;
                        }

                        break;

                    case StockInfo.StockType.cereal:


                        if (objectOnShelf.Count >= cerealPoints.Count)
                        {
                            preventPlacing = true;
                        }

                        break;

                    case StockInfo.StockType.chipsTube:


                        if (objectOnShelf.Count >= tubeChipsPoints.Count)
                        {
                            preventPlacing = true;
                        }

                        break;

                    case StockInfo.StockType.fruit:


                        if (objectOnShelf.Count >= fruitPoints.Count)
                        {
                            preventPlacing = true;
                        }

                        break;

                    case StockInfo.StockType.fruitlarge:


                        if (objectOnShelf.Count >= LargefruitPoints.Count)
                        {
                            preventPlacing = true;
                        }

                        break;







                }




              

            }

        }


        if (preventPlacing == false)
        {
            //objectToPlace.transform.SetParent(transform);

            objectToPlace.makePlaced();



            switch (info.typeOfStock)
            {
                case StockInfo.StockType.bigDrink:


                    objectToPlace.transform.SetParent(bigDrinkPoints[objectOnShelf.Count]);

                    break;

                case StockInfo.StockType.cereal:

                    objectToPlace.transform.SetParent(cerealPoints[objectOnShelf.Count]);

                    break;

                case StockInfo.StockType.chipsTube:


                    objectToPlace.transform.SetParent(tubeChipsPoints[objectOnShelf.Count]);
                    break;

                case StockInfo.StockType.fruit:


                    objectToPlace.transform.SetParent(fruitPoints[objectOnShelf.Count]);

                    break;

                case StockInfo.StockType.fruitlarge:


                    objectToPlace.transform.SetParent(LargefruitPoints[objectOnShelf.Count]);

                    break;
            }



                    //amountOnShelf += 1;
                    objectOnShelf.Add(objectToPlace);

                    //shelfLabel.text = "$" + objectOnShelf[0].info.price;


            UpdateDisplayPrice(info.currentPrice);
        }
    }


    public StockObject GetStock()
    {
        StockObject objectToReturn = null;
        if(objectOnShelf.Count > 0)
        {
            objectToReturn = objectOnShelf[objectOnShelf.Count - 1];
            objectOnShelf.RemoveAt(objectOnShelf.Count - 1);



        }

        if(objectOnShelf.Count == 0)
        {
            shelfLabel.text = string.Empty;
        }
        
        return objectToReturn;


    }


    public void StartPriceUpdate()
    {
        if (objectOnShelf.Count > 0)
        {
            UIController.instance.OpenUpDatePrice(info);



        }


    }


    public void UpdateDisplayPrice(float price)
    {



        if (objectOnShelf.Count > 0)
        {
            info.price = price;
            
            shelfLabel.text = "$" + info.currentPrice.ToString("F2");


        }
    }


}
