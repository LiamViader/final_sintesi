using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rodeta : MonoBehaviour
{


    private Vector3 PuntClic;
    private Collider col;

    private float AngleOff;
    private bool change = false;
    private bool onclic = false;

    private float Rotacio;

    private Camera cam;

    private Vector3 screenPos;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
       col = GetComponent<Collider>();
    }

    // Update is called once per frame

    private 

    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        
        if (Input.GetMouseButtonDown(0))
        {   
            onclic = true;
            //Debug.Log("Mouse pos:" + mousePos);
            //Debug.Log("Centre obj:" + transform.position);
            if (col == Physics2D.OverlapPoint(mousePos)){
                screenPos = cam.WorldToScreenPoint(transform.position);
                Vector3 vec3 = Input.mousePosition - screenPos;
                
                AngleOff = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(vec3.y, vec3.x)) * Mathf.Rad2Deg;
                //Valors rot al fer clic
        }
        if (Input.GetMouseButton(0)){ 
             if (col == Physics2D.OverlapPoint(mousePos)){
                Vector3 vec3 = Input.mousePosition - screenPos;
                float angle = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0,0, angle + AngleOff);
             }
        }
        /*
        if (change && onclic){
            Vector3 PuntActual =  Vector3.Normalize(mousePos - transform.position);
            //
            float angle = Vector3.SignedAngle(PuntAnterior, PuntActual, Vector3.forward);
            Rotacio += angle;
          
            //transform.Rotate(Vector3.forward,angle);
        }
        */
            
    }
     
    void OnMouseOver(){
        change = true;
    }

    void OnMouseExit(){
       change = false;
    }
    
    }
}
