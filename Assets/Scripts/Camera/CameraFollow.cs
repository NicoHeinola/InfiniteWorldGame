using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject followObj;
    public float smoothSpeed = 0.25f;
    public Vector3 offset;
    private Vector3 velocity = new Vector3();
    private PlayerMovementController moveController;
    private void Start()
    {
        this.moveController = followObj.GetComponent<PlayerMovementController>();
    }

    private void LateUpdate()
    {
        Vector3 toPosition = followObj.transform.position;
        if (moveController.moving)
        {
            if (moveController.dirX == -1)
            {
                toPosition.x += offset.x * -1;
            }
            else if (moveController.dirX == 1)
            {
                toPosition.x += offset.x;
            }
            if (moveController.dirY == -1)
            {
                toPosition.y += offset.y * -1;
            }
            else if (moveController.dirY == 1)
            {
                toPosition.y += offset.y;
            }

        }

        toPosition.z += offset.z;

        Vector3 newPos = Vector3.SmoothDamp(transform.position, toPosition, ref velocity, smoothSpeed);
        this.transform.position = newPos;
    }
}
