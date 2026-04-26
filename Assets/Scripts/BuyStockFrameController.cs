using UnityEngine;
using TMPro;
public class BuyStockFrameController : MonoBehaviour
{
    public StockInfo info;

    public TMP_Text nameText, priceText, amountInBoxText, boxPriceText, ButtonText;
    public StockBoxController  boxToSpawn;
    private float boxCost;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateInfoFrame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInfoFrame()
    {
        info = StockInfoController.instance.GetInfo(info.name);
        nameText.text = info.name;
        priceText.text = "$" + info.price.ToString("F2");

        int boxAmount = boxToSpawn.GetStockAmount(info.typeOfStock);

        amountInBoxText.text = boxAmount.ToString() + "per Box";
        boxCost = boxAmount * info.price;
        boxPriceText.text = "Box: $" + boxCost.ToString("F2");
        ButtonText.text = "PAY: $" + boxCost.ToString("F2");
        
    }

    public void BuyBox()
    {
        if(StoreController.instance.CheckMoneyAvailable(boxCost))
        {
            StoreController.instance.SpendMoney(boxCost);
            Instantiate(boxToSpawn, StoreController.instance.stockSpawnPoint.position, Quaternion.identity).SetupBox(info);

        }
    }

}
