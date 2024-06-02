using Codice.Client.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows.Speech;

public class PlayerMovement : AbilityBase
{

    private const string AnimatorBlendTreeX = "DirectionX";
    private const string AnimatorBlendTreeY = "DirectionY";


    [SerializeField]
    float movementSpeed = 10f;
    [SerializeField]
    private LayerMask groundLayer; //Remember to change the layer  when they will be completed 







    private void Start()
    {
        playerController.MovePrevented += PreventAbility;
    }
    private void FixedUpdate()
    {


        //uncomment this  to re-enable  old movement
        Move(); 

        //comment this method to disable new movement system 
        //MoveWithForward();

        if (!playerController.IsGamepadActive)
        {
            CalculateForwardWithMousePosition();
        }
        //uncomment this  to re-enable  old movement
        //else  
        //{
        //    SetForwardOnGamepad();
        //}


    }

    #region internal 



    //New type of movement based on player forward for both gamepad and keyboard
    private void MoveWithForward()

    {
        Vector3 finalVelocity;
        Vector3 Controls = playerController.IsGamepadActive ? InputManager.PlayerMovementPad : InputManager.PlayerMovement;

        if (playerController.IsGamepadActive)
        {
            float currentZSpeed = Controls.y * movementSpeed;
            float currentXSpeed = Controls.x * movementSpeed;
            Vector3 newForward = new Vector3(Controls.x, 0, Controls.y);
            finalVelocity = new Vector3(currentXSpeed, 0, currentZSpeed);

           
            if(newForward!=Vector3.zero)
            playerController.SetForward(newForward);
        }
        else
        {
            Vector3 straightVelocity;
            Vector3 strifeVelocity;
            Vector3 speed = new Vector3(Controls.x, 0,Controls.y) * movementSpeed;
         

            straightVelocity = playerController.GetForward() * speed.z;
            strifeVelocity = playerController.GetTransformRight() * speed.x;

            finalVelocity = straightVelocity + strifeVelocity;
        }

        playerController.AnimatorMgnr.AnimateBlendTree(AnimatorBlendTreeX, Controls.x, AnimatorBlendTreeY, Controls.y);

     
        playerController.SetVelocity(finalVelocity);
        

    }



    //OLD MOVING METHODS 
    //Gets the forward by  using the vector 2 from the right axis , and giving the Y to the Z of the player 
    private void SetForwardOnGamepad()
    {
        if (InputManager.RightAxis != Vector2.zero)
        {
            Vector3 newForward = new Vector3(InputManager.RightAxis.x, 0f, InputManager.RightAxis.y);
            playerController.SetForward(newForward);
        }

    }

    //Works both for keyboard and gamepad. Works in combination with the Functions to calculate the Forward 
    private void Move()
    {
        if (playerController.IsGamepadActive)
        {
            Vector2 Direction = InputManager.PlayerMovementPad;
            float currentZSpeed = Direction.y * movementSpeed;
            float currentXSpeed = Direction.x * movementSpeed;



            playerController.SetVelocity(currentXSpeed, currentZSpeed);
        }
        else
        {
            Vector2 Direction = InputManager.PlayerMovement;
            float currentZSpeed = Direction.y * movementSpeed;
            float currentXSpeed = Direction.x * movementSpeed;

            playerController.AnimatorMgnr.SetAnimatorFloat(AnimatorBlendTreeX, Direction.x);
            playerController.AnimatorMgnr.SetAnimatorFloat(AnimatorBlendTreeY, Direction.y);

            playerController.SetVelocity(currentXSpeed, currentZSpeed);

        }


    }






    //Shoots a raycast from the Camera to the mouse position , Then  set the player forward towards the point where the raycast hits the ground   
    private void CalculateForwardWithMousePosition()
    {

        Vector3 mouseScreenPosition = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
        RaycastHit hitInfo;


        //ENABLE THIS TO SEE THE RAYCAST IN THE EDITOR
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);


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

    #endregion




    protected override void PreventAbility()
    {
        throw new NotImplementedException();
    }

    protected override void UnPreventAbility()
    {
        throw new NotImplementedException();
    }
}

