using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement_Controller : MonoBehaviour
{
    [SerializeField] float inputX, inputY;
    [SerializeField] float speed = 20;
    [SerializeField] public CharacterController cc;
    [SerializeField] Vector3 cameraposition, gravity;
    [SerializeField] bool isOnGround = false;
    [SerializeField] float gravityForce = -19;
    [SerializeField] float jumpHeight = 3;

    RaycastHit hit;
    [SerializeField] Vector3 offset; //posição de adição
    [SerializeField] float lenghtcast;
    [SerializeField] float maxDistance;
    [SerializeField] LayerMask layer;
    [SerializeField] GameObject target;
    [SerializeField] Color raycastColor = Color.red;

    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        Vector3 mvp = transform.right * inputX + transform.forward * inputY;

        if (isOnGround == false) 
        { 
          gravity.y += gravityForce * Time.deltaTime;
          speed = speed >= 0 ? speed -= 5 * 2 * Time.deltaTime : speed = 0;
        }

        if (Input.GetButtonDown("Jump") && isOnGround == true)
        { gravity.y = Mathf.Sqrt(jumpHeight * -2 * gravityForce);}

        cc.Move(mvp * speed * Time.deltaTime);
        cc.Move(gravity * Time.deltaTime);

        //creating a vector = x,y,z
        //pass vector trhought front(para frente)
        Vector3 fwd = transform.TransformDirection(Vector3.up);
        //create a ray(raycast)
        if (Physics.Raycast(transform.position, fwd * lenghtcast, out hit, maxDistance, layer))
        {
            target = hit.collider.gameObject;
            isOnGround = true;
            gravity.y = 0;
            speed = speed <= 20 ? speed += 5 * 2 * Time.deltaTime : speed = 20;
        }
        else { target = null; isOnGround = false; }
        //debug ray on gizmoz
        Debug.DrawRay(transform.position, fwd * lenghtcast, raycastColor);
    }
}
