using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{

    public PlayerAttackController atkController;
    public PlayerMovementController movementController;
    public BuildController buildController;

    // Start is called before the first frame update E
    void Start()
    {
        atkController = GetComponent<PlayerAttackController>();
        movementController = GetComponent<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Attacks
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Get mouse position in scene
            Vector2 positionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.atkController.Punch(positionMouse);
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            this.buildController.Build();
        }


        int dirX = 0;
        int dirY = 0;
        // Vertical
        if (Input.GetKey(KeyCode.S))
        {
            dirY = -1;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            dirY = 1;
        }

        // Horizontal
        if (Input.GetKey(KeyCode.A))
        {
            dirX = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dirX = 1;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            this.movementController.Dash(dirX, dirY);
        }
        else
        {
            this.movementController.Move(dirX, dirY);
        }

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            this.atkController.Punch(dirX, dirY);
        }*/

    }
}
