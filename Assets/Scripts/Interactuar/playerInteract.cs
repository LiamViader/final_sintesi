using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteract : MonoBehaviour
{
    private float cdInteraccio=0f;
    private void Update()
    {
        if (cdInteraccio > 0) cdInteraccio -= Time.deltaTime;
        else
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                float interactRange = 2f;
                Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
                foreach (Collider collider in colliderArray)
                {
                    if (collider.TryGetComponent(out Interactuable meshInteractuable))
                    {
                        meshInteractuable.Interact();
                    }
                }
                cdInteraccio = 0.5f;
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
