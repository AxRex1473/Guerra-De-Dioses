using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed;
    public float panSpeed;
    public float zoomSpeed;
    public float minY;
    public float maxY;
    public float minX; 
    public float maxX; 
    public float minZ; 
    public float maxZ; 


    private void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w"))
        {
            pos.x += panSpeed * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey("s"))
        {
            pos.x -= panSpeed * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey("d"))
        {
            pos.z -= panSpeed * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey("a"))
        {
            pos.z += panSpeed * Time.deltaTime * moveSpeed;
        }

        if (Input.GetKey("q"))
        {
            pos.y += zoomSpeed * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey("e"))
        {
            pos.y -= zoomSpeed * Time.deltaTime * moveSpeed;
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;
    }
}
