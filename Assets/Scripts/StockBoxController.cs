using UnityEngine;
using System.Collections.Generic;

public class StockBoxController : MonoBehaviour
{
    public StockInfo info;
    public List<StockObject> objectOnShelf;
    public List<Transform> bigDrinkPoints, cerealPoints, tubeChipsPoints, fruitPoints, LargefruitPoints;

    public List<StockObject> stockInBox;

    public bool testFill;
    public Rigidbody theRB;
    public Collider col;
    private bool isHeld;
    public float moveSpeed = 5f;
    public GameObject flap1, flap2;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (testFill == true)
        {
            SetupBox(info);
        }

        if (isHeld == true) 
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, moveSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, moveSpeed * Time.deltaTime);



        }



    }


    public void SetupBox(StockInfo stocktype)
    {
        info = stocktype;

        List<Transform> activePoints = new List<Transform>();

        switch (info.typeOfStock)
        {

            case StockInfo.StockType.bigDrink:
                activePoints.AddRange(bigDrinkPoints);

                break;
            case StockInfo.StockType.cereal:
                activePoints.AddRange(cerealPoints);

                break;
            case StockInfo.StockType.chipsTube:
                activePoints.AddRange(tubeChipsPoints);

                break;
            case StockInfo.StockType.fruit:
                activePoints.AddRange(fruitPoints);

                break;
            case StockInfo.StockType.fruitlarge:
                activePoints.AddRange(LargefruitPoints);

                break;




        }

        if (stockInBox.Count == 0)
        {
            for (int i = 0; i < activePoints.Count; i++)
            {
                StockObject stock = Instantiate(stocktype.stockobject, activePoints[i]);


                stock.transform.localPosition = Vector3.zero;
                stock.transform.localRotation = Quaternion.identity;

                stockInBox.Add(stock);

                stock.PlaceInBox();
            }
        }

    }

    public void pickUp()
    {
        theRB.isKinematic = true;

       
        col.enabled = false;
        isHeld = true;
    }


    public void Release()
    {
        theRB.isKinematic = false;
        col.enabled = true;
        isHeld = false;

    }

    public void OpenClose()
    {
        if(flap1.activeSelf == true)
        {
            flap1.SetActive(false);
            flap2.SetActive(false);

        }
        else
        {
            flap1.SetActive(true);
            flap2.SetActive(true);



        }
    }

    public void PlaceStockOnShelf(ShelfSpaceController shelf)
    {
        if(stockInBox.Count > 0)
        {
            shelf.PlaceStock(stockInBox[stockInBox.Count - 1]);

            if(stockInBox[stockInBox.Count - 1].isPlaced == true)
            {
                stockInBox.RemoveAt(stockInBox.Count - 1);
            }
        }


        if(flap1.activeSelf == true)
        {
            OpenClose();
        }


    }

    public int GetStockAmount(StockInfo.StockType type)
    {
        int toReturn = 0;

        switch(type)
        {
            case StockInfo.StockType.bigDrink:
               toReturn = bigDrinkPoints.Count;

                break;
            case StockInfo.StockType.cereal:
                toReturn = cerealPoints.Count;

                break;
            case StockInfo.StockType.chipsTube:
                toReturn = tubeChipsPoints.Count;

                break;
            case StockInfo.StockType.fruit:
                toReturn = fruitPoints.Count;

                break;
            case StockInfo.StockType.fruitlarge:
                toReturn = LargefruitPoints.Count;

                break;

        }

        return toReturn;
    }

}
