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
                    if (collider.TryGetComponent(out MeshInteractuable meshInteractuable))
                    {
                        meshInteractuable.Interact();
                    }
                }
                cdInteraccio = 0.5f;
            }
        }

    }


public MeshInteractuable GetInteractuableObject(){
        List<MeshInteractuable> meshInteractuableList = new List<MeshInteractuable>();
        float interactRange = 2f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach(Collider collider in colliderArray){
                if(collider.TryGetComponent(out MeshInteractuable meshInteractuable)){
                    meshInteractuableList.Add(meshInteractuable);
                }
            }
            MeshInteractuable closestMesh = null;
            foreach(MeshInteractuable mesh in meshInteractuableList){
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
