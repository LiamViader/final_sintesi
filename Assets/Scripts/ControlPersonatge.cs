using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//const double vel_caminar=0.1;
//const double vel_correr=0.4;


//vec Direcci� --> �s un vector normalitzat que indica cap a on mira el personatge
/*Personatge{
    Vector3 dir = new Vector3(0,1,0);
    double vel;
    bool moviment;
}*/
public class ControlPersonatge : MonoBehaviour
{
    private Rigidbody rb;
    public float forceSaltar = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.DKey))
        {
            Personatge.dir = Personate.dir + Vector3(0, 1, 0);
            Personatge.dir.normalize();
            Personatge.vel = vel_caminar;
            moviment = true;
        }
        else if (Input.GetKey(KeyCode.AKey))
        {
            Personatge.dir = Personate.dir + Vector3(0, -1, 0);
            Personatge.dir.normalize();
            Personatge.vel = vel_caminar;
            moviment = true;
        }
        else if (Input.GetKey(KeyCode.Skey))
        {
            Personatge.dir = Personate.dir + Vector3(-1, 0, 0);
            Personatge.dir.normalize();
            Personatge.vel = vel_caminar;
            moviment = true;

        }
        else if (Input.GetKey(KeyCode.WKey))
        {
            Personatge.dir = Personate.dir + Vector3(1, 0, 0);
            Personatge.dir.normalize();
            Personatge.vel = vel_caminar;
            moviment = true;
        }
        else moviment = false;
        if (Input.GetKey(KeyCode.Shiftkey))
        {
            Personatge.vel = vel_correr;
            moviment = true;
        }
        if (moviment)
        {
            Update_position(Personatge.dir * Personatge.vel);
        }*/
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up*forceSaltar, ForceMode.Impulse);
            /*if (esPetit)
            {
                //salta m�s
                rigidbody.AddForce(Vector3.up*forceSaltar, ForceMode.Impulse);
            }
            else
            {
                //salta menys
                rigidbody.AddForce();
            }*/
        }
    }
}
