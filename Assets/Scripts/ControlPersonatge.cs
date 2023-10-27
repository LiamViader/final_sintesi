using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class ControlPersonatge : MonoBehaviour
{
    //public CharacterController jugador;
    
    public float forceSaltar = 5.0f;
    public float velCaminar = 10.0f;
 
    public Transform _Camera;

    public float TempsSmooth = 0.1f;
    float VelSmooth;

    float horitzontalInput;
    float verticalInput;

    Vector3 Direccio;

    

    private Rigidbody rb;

    private bool saltant = false;
   




    // Start is called before the first frame update
    void Start()
    {
      //  if (_Camera == null) _Camera = GameObjects.FindGameObjectWithTag("MainCamera");
        rb = GetComponent<Rigidbody>();
    }
   
    //agafar els valors de control
    private void Control()
    {   
        //dreta esquerra , valoe entre -1 i 1
        horitzontalInput = Input.GetAxisRaw("Horizontal");
        //endavant darere valor entre -1 i 1
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    //Moviemnt, canviar direcció personatge aplicar velocitat i 
    private void Moviment()
    {
        Direccio = new Vector3(horitzontalInput, 0, verticalInput).normalized; //* orientacio.forward;
        //movDir.normalized;
        if (Direccio.magnitude >= 0.1)
        {
            float gir = Mathf.Atan2(Direccio.x, Direccio.z) * Mathf.Rad2Deg + _Camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, gir, ref VelSmooth, TempsSmooth);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            
            Vector3 movDir = Quaternion.Euler(0,gir,0)* Vector3.forward;
            rb.velocity = movDir.normalized * velCaminar * Time.deltaTime;
        }
       
        //orientacio.transform = movDir;
    }


    // Update is called once per frame
    void Update()
    {
       // _Camera.LookAt(rb);
        Control();
        Moviment();
        if (Input.GetKeyDown(KeyCode.Space) && !saltant)
        {
            rb.AddForce(Vector3.up*forceSaltar, ForceMode.Impulse);
            saltant = true;
            /*if (esPetit)
            {
                //salta m�s
                rb.AddForce(Vector3.up*forceSaltar, ForceMode.Impulse);
            }
            else
            {
                //salta menys
                rb.AddForce();
            }*/
        }
    }
}
