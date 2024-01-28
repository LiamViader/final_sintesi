using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine;

public class ComprovaButton : MonoBehaviour
{
    // Start is called before the first frame update
    private Collider col;

    [SerializeField] 
    public UnityEvent correcte;
    [SerializeField] 
    public UnityEvent incorrecte;

    Functions_Ones OnaDin, OnaEst;
    void Start()
    {
        col = GetComponent<Collider>();   
        OnaDin = GameObject.Find("Ona dinamica").GetComponent<Functions_Ones>();     
        OnaEst = GameObject.Find("Ona estatica").GetComponent<Functions_Ones>();    
        col.enabled =false;
    }

    // Update is called once per frame
    void OnMouseDown()
    {
     
        float A = 0;
        float F = 0;
        float A2 = 0;
        float F2 = 0;
        Debug.Log("PREMUT");
        OnaDin.GetDades(out A, out F);
        OnaEst.GetDades(out A2, out F2);
        Debug.Log ("Amplitud 1 ->>"+A + "   Fase1 -->" + F);
        Debug.Log ("Amplitud 2 ->>"+A2 + "   Fase2 -->" + F2);
        if (OnaEst.Igual(A,F)){
            Debug.Log("IGUAL");
            correcte.Invoke();
        }
        else{
            incorrecte.Invoke();
        }
    }
            
        
        
    
}
