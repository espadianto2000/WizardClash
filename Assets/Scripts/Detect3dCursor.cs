using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class Detect3dCursor : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    public float zoomSpeed;
    public float moveSpeed;
    public bool isDragging = false;
    public float totalMovement = 0;
    public float minDistance;
    public float maxDistance;

    private void FixedUpdate()
    {
        //500 4 -1 6.5     
        if (isDragging)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            Vector3 movement = new Vector3(mouseX, 0f, mouseY);
            transform.Translate(-movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) // 0 refers to the left mouse button
        {
            isDragging = true;
        }

        // Check for mouse button release
        if (Input.GetMouseButtonUp(1)) // 0 refers to the left mouse button
        {
            isDragging = false;
        }
        moveSpeed = 420 - (totalMovement + 20) * 2f;
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        Vector3 forwardDirection = mainCamera.transform.forward;
        if ((totalMovement >= maxDistance && scrollInput > 0) || (totalMovement <= minDistance && scrollInput < 0) || scrollInput == 0)
        {

        }
        else
        {
            float movementAmount = zoomSpeed * Time.fixedDeltaTime * scrollInput;
            float previousMovement = totalMovement;
            totalMovement += movementAmount;
            if (totalMovement <= minDistance)
            {
                totalMovement = minDistance;
                movementAmount = minDistance - previousMovement;
            }
            else if (totalMovement >= maxDistance)
            {
                totalMovement = maxDistance;
                movementAmount = maxDistance - previousMovement;
            }
            mainCamera.transform.Translate(forwardDirection * movementAmount, Space.World);
        }
    }
    private bool IsCursorOverUI()
    {
        // Create a pointer event data
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        // Check for UI raycast
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        // If any UI objects were hit, the cursor is over a UI object
        return results.Count > 0;
    }
}
