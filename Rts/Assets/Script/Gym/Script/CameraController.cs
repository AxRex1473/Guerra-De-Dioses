using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float panSpeed = 20f;
    [SerializeField] float zoomSpeed = 5f;
    [SerializeField] float minY = 10f;
    [SerializeField] float maxY = 80f;

    private void Update()
    {
        Vector3 pos = transform.position;

        // Camera Movement
        if (Input.GetKey("w"))
        {
            pos.z += panSpeed * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey("s"))
        {
            pos.z -= panSpeed * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey("d"))
        {
            pos.x += panSpeed * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= panSpeed * Time.deltaTime * moveSpeed;
        }

        // Camera Zoom
        if (Input.GetKey("q"))
        {
            pos.y += zoomSpeed * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey("e"))
        {
            pos.y -= zoomSpeed * Time.deltaTime * moveSpeed;
        }

        transform.position = pos;
    }
}
