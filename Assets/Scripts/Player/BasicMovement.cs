using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasicMovement : MonoBehaviour
{
    private CharacterController Controller;
    private AnimationManager AnimManager;

    [SerializeField] private GameObject AimTarget;
    [SerializeField] private GameObject GroundPlace;

    private float CameraRotationY;
    private float CameraRotationX;
    private const float RotationSpeed = 3f;
    
    private const float WalkingSpeed = 5f;
    private LayerMask Ground;

    private bool IsTrapped = false;
    private const float CheckGroundRadius = 0.3f;

    private float NormalGravity = -2.0f;
    private float CurrentGravity = 0.0f;
    

    void Start()
    {

        PlayerManager.Instance.Movment = this;
        Controller = GetComponent<CharacterController>();
        AnimManager = GetComponent<AnimationManager>();

        Ground = LayerMask.GetMask("Ground");

    }


    void Update()
    {
        //if player is not trapped or in menu
        if(!IsTrapped)
        {
            Move();
            UpdateRotation();
        }
    }



    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float gravity;

        AnimManager.SetMovingBlend(x, z);
        
        //control basic gravity
        if (!CheckGround())
        {
            gravity = CurrentGravity -= 1 * Time.deltaTime;
        }
        else
        {
            CurrentGravity = NormalGravity;
            gravity = NormalGravity;
        }

        Vector3 move = transform.right * x + transform.forward * z + transform.up * gravity;

        Controller.Move(move * WalkingSpeed * Time.deltaTime);
        
    }
    
    

    private void UpdateRotation()
    {
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");

        CameraRotationY -= MouseY * RotationSpeed;
        CameraRotationY = Mathf.Clamp(CameraRotationY, -90.0f, 90.0f);
        CameraRotationX = Mathf.Clamp(CameraRotationX, -100, 100);

        //Rotate the player body to the last wall running angle or slide
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //Player can look around when wall running or slide
            CameraRotationX -= MouseX * RotationSpeed;
            AimTarget.transform.localEulerAngles = Vector3.down * CameraRotationX + Vector3.right * CameraRotationY;
        }
        else
        {
            if (CameraRotationX != 0)
            {
                
                Quaternion Rotation = new Quaternion(transform.rotation.x, AimTarget.transform.rotation.y, transform.rotation.z, AimTarget.transform.rotation.w);
                transform.rotation = Rotation;
                CameraRotationX = 0.0f;
            }

            //Rotate the body (left right) and the head (up down)
            transform.Rotate(Vector3.up * MouseX * RotationSpeed);
            AimTarget.transform.localEulerAngles = Vector3.right * CameraRotationY;
        }

    }

    private bool CheckGround()
    {
        //make it a sphere for better detection when ground is small
        return (Physics.CheckSphere(GroundPlace.transform.position, CheckGroundRadius, Ground));
    }


    //----------------------------------------------------------------------------// Called by traps //------------------------------------------------------------------------------------------------//
    public void GetStagger()
    {
        AnimManager.PlayTrapped();
        StartCoroutine("Stagger");
        IsTrapped = true;
    }

    private IEnumerator Stagger()
    {
        yield return new WaitForSeconds(2);
        AnimManager.StopTrapped();
        IsTrapped = false;
    }
    
    

}
