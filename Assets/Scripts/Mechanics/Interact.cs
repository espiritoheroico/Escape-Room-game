using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    #region vars
    [SerializeField] float maxdis = 10;
    [SerializeField] float lenghtcast = 10;
    public RaycastHit rh;
    public GameObject target;
    [SerializeField] Color raycastColor = Color.red;
    [SerializeField] LayerMask layer;
    #endregion

    #region slots
    [SerializeField]Inventory inventory;
    #endregion

    void Update()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        //create a ray(raycast)
        if (Physics.Raycast(transform.position, fwd * lenghtcast, out rh, maxdis, layer))
        {
            target = rh.collider.gameObject;
        }
        //debug ray on gizmoz
        Debug.DrawRay(transform.position, fwd * lenghtcast, raycastColor);
    }

    void SetInventory(Inventory i)
    {
        this.inventory = i;
    }
}
