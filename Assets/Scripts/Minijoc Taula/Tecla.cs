using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tecla : MonoBehaviour
{
    public UnityEvent OnClic;
    void Start()
    {
        
    }

    // Update is called once per frame

    void OnMouseDown(){
        OnClic.Invoke();
    }
}
