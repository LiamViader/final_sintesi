using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteract : MonoBehaviour
{
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            HashSet<Interactuable> interactuats = new HashSet<Interactuable>();
            float interactRange = 2f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out Interactuable meshInteractuable))
                {
                    if (!interactuats.Contains(meshInteractuable)) {
                        meshInteractuable.Interact();
                        interactuats.Add(meshInteractuable);
                    }
                }
            }
        }

    }


public Interactuable GetInteractuableObject(){
        List<Interactuable> meshInteractuableList = new List<Interactuable>();
        float interactRange = 2f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach(Collider collider in colliderArray){
                if(collider.TryGetComponent(out Interactuable meshInteractuable)){
                    meshInteractuableList.Add(meshInteractuable);
                }
            }
            Interactuable closestMesh = null;
            foreach(Interactuable mesh in meshInteractuableList){
                if (closestMesh == null)
                {
                    closestMesh = mesh;
                } else
                {
                    if (Vector3.Distance(transform.position, mesh.transform.position) < 
                        Vector3.Distance(transform.position, closestMesh.transform.position))
                    {
                        closestMesh = mesh;
                    }
                }
            }
        return closestMesh;
    }
}
