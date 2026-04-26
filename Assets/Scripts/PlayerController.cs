using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public InputActionReference moveAction;
    public InputActionReference jumpAction; 
    public CharacterController carCom;
    public float moveSpeed;
    private float ySpeed;
    public float jumpForce;
    public InputActionReference lookAction;
    private float horRot, verRot;
    public float lookSpeed;
    public Camera theCam;
    public float minLookAngle, maxLookAngle;
    public LayerMask whatIsStock;
    public float interactionRange;

    private StockObject heldPick;
    public Transform holdPoint;
    public float throwForce;

    public LayerMask whatIsShelf;
    public LayerMask whatIsStockBox;
    public StockBoxController heldBox;
    public Transform boxHoldPoint;
    public float waitToPlaceStock;
    private float placeStockCounter;
    public LayerMask whatIsBin;
    public LayerMask whatIsFurniture;
    public Transform furniturePoint;
    public FurnitureController HeldFurniture;
    public LayerMask whatIsCheckout;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (UIController.instance.UpdatePricePanel != null)
        {
            if (UIController.instance.UpdatePricePanel.activeSelf == true)
            {
                return;
            }
        }

        if (UIController.instance.buyMenuScreen != null)
        {
            if (UIController.instance.buyMenuScreen.activeSelf == true)
            {
                return;
            }
        }

        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();
        horRot += lookInput.x * Time.deltaTime * lookSpeed;
        transform.rotation = Quaternion.Euler(0f, horRot, 0f);

        verRot -= lookInput.y * Time.deltaTime * lookSpeed;
        verRot = Mathf.Clamp(verRot, minLookAngle, maxLookAngle);
        theCam.transform.localRotation = Quaternion.Euler(verRot, 0f, 0f);





        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();

        //transform.position = transform.position + new Vector3(moveInput.x * Time.deltaTime *moveSpeed, 0f, moveInput.y * Time.deltaTime * moveSpeed 
        //Vector3 moveAmount = new Vector3(moveInput.x, 0f, moveInput.y);

        Vector3 vertMove = transform.forward * moveInput.y;
        Vector3 horiMove = transform.right * moveInput.x;
        Vector3 moveAmount = horiMove + vertMove;
        moveAmount = moveAmount.normalized;
        moveAmount = moveAmount * moveSpeed;

        if (carCom.isGrounded == true)
        {


            ySpeed = 0f;

            if (jumpAction.action.WasPressedThisFrame())
            {
                ySpeed = jumpForce;
            }


        }
        ySpeed = ySpeed + (Physics.gravity.y * Time.deltaTime);


        moveAmount.y = ySpeed;

        carCom.Move(moveAmount * Time.deltaTime);

        //CHECK FOR PICK UP

        Ray ray = theCam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        RaycastHit hit;

        // if (Physics.Raycast(ray, out hit, interactionRange, whatIsStock))
        // {
        //     Debug.Log("I SEE");
        // }
        // else
        // {
        //     Debug.Log("iDontSee");
        // }

        if (heldPick == null && heldBox == null && HeldFurniture == null)
        {



            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (Physics.Raycast(ray, out hit, interactionRange, whatIsStock))
                {
                    // heldPick = hit.collider.gameObject;
                    // heldPick.transform.SetParent(holdPoint);
                    // heldPick.transform.localPosition = Vector3.zero;
                    // heldPick.transform.localRotation = Quaternion.identity;

                    // heldPick.GetComponent<Rigidbody>().isKinematic = true;
                    heldPick = hit.collider.GetComponent<StockObject>();
                    heldPick.transform.SetParent(holdPoint);
                    heldPick.pickUp();
                    return;



                }

                if (Physics.Raycast(ray, out hit, interactionRange, whatIsStockBox))
                {
                    heldBox = hit.collider.GetComponent<StockBoxController>();
                    heldBox.transform.SetParent(boxHoldPoint);
                    heldBox.pickUp();

                    if(heldBox.flap1.activeSelf == true)
                    {
                        heldBox.OpenClose();

                    }

                    return;
                }
                if (Physics.Raycast(ray, out hit, interactionRange, whatIsCheckout))
                {
                    hit.collider.GetComponent<CheckOut>().CheckOutCustomer();
                    

                }


            }

            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                if (Physics.Raycast(ray, out hit, interactionRange, whatIsShelf))
                {
                    heldPick = hit.collider.GetComponent<ShelfSpaceController>().GetStock();

                    if (heldPick != null)
                    {
                        heldPick.transform.SetParent(holdPoint);
                        heldPick.pickUp();

                    }
                    return;

                }
                if (Physics.Raycast(ray, out hit, interactionRange, whatIsStockBox))
                {

                    hit.collider.GetComponent<StockBoxController>().OpenClose();


                }


            }

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                if (Physics.Raycast(ray, out hit, interactionRange, whatIsShelf))
                {
                    hit.collider.GetComponent<ShelfSpaceController>().StartPriceUpdate();
                }
            }

            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                if (Physics.Raycast(ray, out hit, interactionRange, whatIsFurniture))
                {
                    HeldFurniture = hit.transform.GetComponent<FurnitureController>();
                    HeldFurniture.transform.SetParent(furniturePoint);
                    HeldFurniture.transform.localPosition = Vector3.zero;
                    HeldFurniture.transform.localRotation = Quaternion.identity;

                    HeldFurniture.MakePlacable();

                }
            }


        }
        else
        {

            if (heldPick != null)
            {





                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    if (Physics.Raycast(ray, out hit, interactionRange, whatIsShelf))
                    {

                        // heldPick.transform.position = hit.transform.position;
                        // heldPick.transform.rotation = hit.transform.rotation;


                        // heldPick.transform.SetParent(null);
                        // heldPick = null;

                        // heldPick.makePLaced();
                        // heldPick.transform.SetParent(hit.transform);
                        // heldPick = null;

                        hit.collider.GetComponent<ShelfSpaceController>().PlaceStock(heldPick);

                        if (heldPick.isPlaced == true)
                        {
                            heldPick = null;
                        }

                    }



                }



                if (Mouse.current.rightButton.wasPressedThisFrame)
                {
                    // Rigidbody pickupRB = heldPick.GetComponent<Rigidbody>();
                    // pickupRB.isKinematic = false;


                    heldPick.Release();
                    heldPick.theRB.AddForce(theCam.transform.forward * throwForce, ForceMode.Impulse);
                    heldPick.transform.SetParent(null);
                    heldPick = null;

                }
            }

            if (heldBox != null)
            {

                if (Mouse.current.rightButton.wasPressedThisFrame)
                {
                    

                    heldBox.Release();
                    heldBox.theRB.AddForce(theCam.transform.forward * throwForce, ForceMode.Impulse);
                    heldBox.transform.SetParent(null);
                    heldBox = null;

                }
                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    heldBox.OpenClose();
                }





                if (Mouse.current.leftButton.wasPressedThisFrame)
                {


                    if(heldBox.stockInBox.Count > 0)
                    {





                        if (Physics.Raycast(ray, out hit, interactionRange, whatIsShelf))
                        {
                            heldBox.PlaceStockOnShelf(hit.collider.GetComponent<ShelfSpaceController>());


                            placeStockCounter = waitToPlaceStock;
                        }

                    }
                    else
                    {
                        if (Physics.Raycast(ray, out hit, interactionRange, whatIsBin))
                        {
                            Destroy(heldBox.gameObject);
                            heldBox = null;


                        }
                    }


                }


                if (Mouse.current.leftButton.isPressed)
                {
                    placeStockCounter -= Time.deltaTime;

                    if(placeStockCounter <= 0)
                    {
                        if (Physics.Raycast(ray, out hit, interactionRange, whatIsShelf))
                        {
                            heldBox.PlaceStockOnShelf(hit.collider.GetComponent<ShelfSpaceController>());


                            placeStockCounter = waitToPlaceStock;
                        }
                    }


                }


            }
            if (HeldFurniture != null)
            {
                HeldFurniture.transform.position = new Vector3(furniturePoint.position.x, 0f, furniturePoint.position.z);
                HeldFurniture.transform.LookAt(new Vector3(transform.position.x, 0f, transform.position.z));

                if(Mouse.current.leftButton.wasPressedThisFrame || Keyboard.current.rKey.wasPressedThisFrame)
                {
                    HeldFurniture.transform.SetParent(null);
                    HeldFurniture.PlaceFurniture();
                    
                    HeldFurniture = null;

                } 

            }

        }







    }


}
