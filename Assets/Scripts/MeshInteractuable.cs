using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshInteractuable : MonoBehaviour
{
    [SerializeField] private string interactText;
    public void Interact() {
        Debug.Log("Interact!");
    }

    public string returnInteractText(){
        return interactText;
    }
}
