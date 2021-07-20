using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Player_Controller : MonoBehaviour
{
    #region vars
    [SerializeField] float inputX, inputY; //input controls
    [SerializeField] float mouseX, mouseY;
    [SerializeField] float sensibility = 100;
    [SerializeField] float rotationX, rotationY,maxdis,lenghtcast;
    float dtime;
    //raycast to objects
    RaycastHit rh;
    [SerializeField] Color raycastColor = Color.red;
    [SerializeField] public Transform body;
    [SerializeField] GameObject target;
    [SerializeField] LayerMask layer;
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
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        //create a ray(raycast)
        if(Physics.Raycast(transform.position,fwd*lenghtcast,out rh, maxdis, layer))
        {
            target = rh.collider.gameObject;
        }
        //debug ray on gizmoz
        Debug.DrawRay(transform.position, fwd * lenghtcast, raycastColor);
    }
}