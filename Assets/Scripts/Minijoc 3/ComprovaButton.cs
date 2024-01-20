using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine;

public class ComprovaButton : MonoBehaviour
{
    // Start is called before the first frame update
    private Collider col;

    public UnityEvent correcte;

    private GameObject OnaDin;
    private GameObject OnaEst;
    void Start()
    {
        col = GetComponent<Collider>();   
        OnaDin = GameObject.Find("Ona dinamica");     
        OnaEst = GameObject.Find("Ona estatica");    
    }

    // Update is called once per frame
    void Update()
    {
         Vector3 mousePos = Camera.main.transform.position;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit)){
            mousePos = raycastHit.point; 
            if (Input.GetMouseButtonDown(0)){ 
                float A = 0;
                float F = 0;
                OnaDin.Functions_Ones.GetDades(A,F);
                if (OnaEst.Igual(A,F)){
                    correcte.Invoke();
                }
            }
        }
        
    }
}
