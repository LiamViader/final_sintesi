using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rodeta : MonoBehaviour
{


    private Mesh Malla;
    float NewRotacio = 0;
    float RotInicial = 0;
    float RotDif;
    private bool change = false;
    // Start is called before the first frame update
    void Start()
    {
       Malla = GetComponent<Mesh>(); 
    }

    void OnMouseOver()
    {
       NewRotacio = Input.mouseScrollDelta.y;
        RotDif = NewRotacio-RotInicial;
        RotInicial = NewRotacio;
        this.transform.Rotate(new Vector3(0,0,RotDif));

    }

    // Update is called once per frame


    void Update()
    {

            
    }
}
