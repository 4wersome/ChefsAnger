using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : AbilityBase
{

    [SerializeField]
    float movementSpeed = 1000f;
    [SerializeField]
    private LayerMask groundLayer; //Remember to change the layer  when they will be completed 
    [SerializeField]
    private bool isGamepadActive;


    private float HorizontalmovementAction;
    private float VerticalmovementAction;






    private void FixedUpdate()
    {
        //Gamepad Is Enabled if no key is pressed and one of the two axis is not equal  0 
        isGamepadActive = !Keyboard.current.anyKey.isPressed & InputManager.RightAxis!=Vector2.zero;
        
        Move();

        if (!isGamepadActive)
        {
            CalculateForwardWithMousePosition();
        }
        else
        {
            SetForwardOnGamepad();
        }
    }





    //Gets the forward by  using the vector 2 from the right axis , and giving the Y to the Z of the player 
    private void SetForwardOnGamepad()
    {
        if (InputManager.RightAxis != Vector2.zero)
        {
            Vector3 newForward = new Vector3(InputManager.RightAxis.x, 0f, InputManager.RightAxis.y);
            playerController.SetForward(newForward);
        }

    }




    //Works both for keyboard and gamepad by using new input system
    private void Move()
    {
        Vector2 Direction = InputManager.Player_Movement;
        float currentZSpeed = Direction.y * (movementSpeed * Time.deltaTime);
        float currentXSpeed = Direction.x * (movementSpeed * Time.deltaTime);

        playerController.SetVelocity(currentXSpeed, currentZSpeed);
    }



    //Shoots a raycast from the Camera to the mouse position , Then  set the player forward towards the point where the raycast hits the ground   
    private void CalculateForwardWithMousePosition()
    {

        Vector3 mouseScreenPosition = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
        RaycastHit hitInfo;


        //ENABLE THIS TO SEE THE RAYCAST IN THE EDITOR
        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);  


        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, groundLayer))
        {

            Vector3 pointToLook = hitInfo.point;


            Vector3 directionToLook = pointToLook - transform.position;
            directionToLook.y = 0;

            if (directionToLook != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToLook);
                playerController.SetRotation(targetRotation);
            }

        }

    }

}

