using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MertInput : MonoBehaviour
{
    [Header("Input Speed")]
    public float inputAccelSpeed = 1;
    public float inputDeccelSpeed = 2;
    public float directionChangeSpeed = 2;

    [Header("Key Bindings")]
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;

    public static float horizontal;
    public static float horizontalRaw;
    public static float vertical;
    public static float verticalRaw;

    float inputCurrentHorizontalSpeed = 0;
    float inputCurrentVerticalSpeed = 0;
    float xTarget = 0f;
    float yTarget = 0f;



    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Laver keyPresses om til input
        HorizontalInput();
        VerticalInput();

        //Sætter RawInput
        horizontalRaw = xTarget;
        verticalRaw = yTarget;

        //Sætter Input
        horizontal = Mathf.MoveTowards(horizontal, xTarget, inputCurrentHorizontalSpeed * Time.deltaTime);
        vertical = Mathf.MoveTowards(vertical, yTarget, inputCurrentVerticalSpeed * Time.deltaTime);
    }

    void HorizontalInput()
    {
        if (Input.GetKey(rightKey))
        {
            xTarget = 1f;
            if (horizontal < 0)
            {
                inputCurrentHorizontalSpeed = directionChangeSpeed;
            }
            else
            {
                inputCurrentHorizontalSpeed = inputAccelSpeed;
            }
        }

        else if (Input.GetKey(leftKey))
        {
            xTarget = -1f;
            if (horizontal > 0)
            {
                inputCurrentHorizontalSpeed = directionChangeSpeed;
            }
            else
            {
                inputCurrentHorizontalSpeed = inputAccelSpeed;
            }
        }

        else
        {
            xTarget = 0f;
            inputCurrentHorizontalSpeed = inputDeccelSpeed;
        }
    }

    void VerticalInput()
    {
        if (Input.GetKey(upKey))
        {
            yTarget = 1f;
            if(vertical < 0)
            {
                inputCurrentVerticalSpeed = directionChangeSpeed;
            }
            else
            {
                inputCurrentVerticalSpeed = inputAccelSpeed;
            }
        }

        else if (Input.GetKey(downKey))
        {
            yTarget = -1f;
            if(vertical > 0)
            {
                inputCurrentVerticalSpeed = directionChangeSpeed;
            }
            else
            {
                inputCurrentVerticalSpeed = inputAccelSpeed;
            }
        }

        else
        {
            yTarget = 0f;
            inputCurrentVerticalSpeed = inputDeccelSpeed;
        }
    }
}
