using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Player_Controller : MonoBehaviour
{
    #region vars
    [SerializeField] float inputX, inputY; //input controls
    [SerializeField] float mouseX, mouseY;
    [SerializeField] float sensibility = 100;
    [SerializeField] float rotationX, rotationY;
    float dtime;
    [SerializeField] public Transform body;
    [SerializeField] GameObject target;
    #endregion

    void Update()
    {
        dtime = Time.deltaTime;
        //receive WASD inputs
        mouseX = Input.GetAxis("Mouse X") * sensibility * dtime;
        mouseY = Input.GetAxis("Mouse Y") * sensibility * dtime;
        rotationY = Mathf.Clamp(rotationY, -90, 90);
        //invert mouse movement
        rotationY -= mouseY;
        //change camera axis euler by X = convert to ROTATION Y
        transform.localRotation = Quaternion.Euler(new Vector3(rotationY, 0, 0));
        body.Rotate(Vector3.up * mouseX);
        //enable Raycast
    }
}