using UnityEngine;

public class CameraMovementRTS : MonoBehaviour
{
    public static CameraMovementRTS instance;
    public Transform cameraTransform;

    public float normalSpeed;
    public float fastSpeed;
    public float movementSpeed;
    public float movementTime;
    public float rotationAmount;
    public Vector3 zoomAmount;
    public float minZoom;
    public float maxZoom;


    Vector3 newPosition;
    Quaternion newRotation;
    Vector3 newZoom;

    public GameObject buildingSystem;
    bool active = true;

    void Start()
    {
        instance = this;
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    void Update()
    {
        HandleMovementInput();
    }

    void HandleMovementInput() // Controls for keyword
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }

        //Movement keys ---------------------------------------------------------------------------------------------------------------------------------------------------

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.forward * -movementSpeed);
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * movementSpeed);
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -movementSpeed);
        }

        //Zooom Keys -------------------------------------------------------------------------------------------------------------------------------------------------------

        newZoom.y = Mathf.Clamp(newZoom.y, minZoom, maxZoom);

        if ((newZoom.y == minZoom && Input.GetKey(KeyCode.R)) || (newZoom.y == maxZoom && Input.GetKey(KeyCode.F)))
        {
            return;
        }

        if (Input.GetKey(KeyCode.R))
        {
            newZoom += zoomAmount;
        }
        if (Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomAmount;
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationAmount);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-160f,870f),transform.position.y, Mathf.Clamp(transform.position.z,-120f,900f));

        //UI Building -------------------------------------------------------------------------------------------------------------------------------------------------------

        if (Input.GetKeyDown(KeyCode.Q))
        {
            active = !active;
            buildingSystem.SetActive(active);
        }
    }
}
