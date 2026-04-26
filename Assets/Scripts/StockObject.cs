using UnityEngine;

using UnityEngine.InputSystem;





public class StockObject : MonoBehaviour
{
    public StockInfo info;


    public float moveSpeed2;
    public bool isPlaced;

    public Rigidbody theRB;
    public Collider col;
    private bool inBag;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        info = StockInfoController.instance.GetInfo(info.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaced == true)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, moveSpeed2 * Time.deltaTime);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, moveSpeed2 * Time.deltaTime);
        }
        if(inBag == true)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime);
        }
    }
    public void pickUp()
    {
        theRB.isKinematic = true;

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        isPlaced = false;
        col.enabled = false;
    }

    public void makePlaced()
    {
        theRB.isKinematic = true;
        isPlaced = true;
        col.enabled = false;

    }

    public void Release()
    {
        theRB.isKinematic = false;
        col.enabled = true;

    }


    public void PlaceInBox()
    {
        theRB.isKinematic = true ;
        col.enabled = false;
    }

    public void PlaceInBag()
    {
        inBag = true;
        makePlaced();
    }
}
