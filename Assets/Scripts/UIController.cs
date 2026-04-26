using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{


    public static UIController instance;

    private void Awake()
    {
        instance = this;




    }    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject UpdatePricePanel;

    public TMP_Text basePriceText, currentPriceText;
    public TMP_InputField priceInputField;
    private StockInfo activeStockInfo;
    public TMP_Text moneyText;
    public GameObject buyMenuScreen;
    public GameObject PausePanel;
    private bool isPaused = false;
    public string menuScene;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
             OpenCloseBuyMenu();
        }

        if ( Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused == true)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void OpenUpDatePrice(StockInfo stockToUpdate)
    {
        UpdatePricePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        basePriceText.text = "$" + stockToUpdate.price.ToString("F2");
        currentPriceText.text = "$" + stockToUpdate.currentPrice.ToString("F2");

        activeStockInfo = stockToUpdate;

        priceInputField.text = stockToUpdate.currentPrice.ToString();

    }


    public void CloseUpdatePanel()
    {
        UpdatePricePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }
     
    public void ApplyPriceUpdate()
    {
       

            activeStockInfo.currentPrice = float.Parse(priceInputField.text);

            currentPriceText.text = "$" + activeStockInfo.currentPrice;

            StockInfoController.instance.UpdatePrice(activeStockInfo.name, activeStockInfo.currentPrice);

            CloseUpdatePanel();
       
    }


    public void UpdateMoney(float currentMoney)
    {
        moneyText.text = "$" + currentMoney.ToString("F2");
    }


    public void OpenCloseBuyMenu()
    {
        if(buyMenuScreen.activeSelf == false)
        {
            buyMenuScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;

            buyMenuScreen.SetActive(false);
        }
    }


    public void PauseGame()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        

        Debug.Log("asdfasdf");
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("yeah");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(menuScene);
        Time.timeScale = 1f;
    }
}
