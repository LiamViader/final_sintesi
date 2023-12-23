using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPickupsDrop : MonoBehaviour
{
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private Transform objectGrabPointTransform;

    private ObjectGrabbable objectGrabbable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(objectGrabbable == null){

                float pickUpDistance = 2f;
                if(Physics.Raycast(transform.position, transform.forward, out RaycastHit rayCastHit, pickUpDistance)){
                    if(rayCastHit.transform.TryGetComponent(out objectGrabbable)){
                        objectGrabbable.Grab(objectGrabPointTransform);
                        Debug.Log(rayCastHit.transform);
                    }
                }

            } else
            {
                objectGrabbable.Drop();
                objectGrabbable = null;
            }
            
        }
    }
}
