using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactuable : MonoBehaviour
{
    [SerializeField] protected string interactText;
    [SerializeField] protected string unableText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Interact();

    public string returnInteractText()
    {
        return interactText;
    }

     public string returnUnableText()
    {
        return unableText;
    }
    
}
