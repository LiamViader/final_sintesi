using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;


public class ControlPersonatge : MonoBehaviour
{
    //public CharacterController jugador;
    
    public float forceSaltar = 5.0f;
    public float velCaminar = 7.0f;
    public float velCorrer = 12.0f;
 
    public Transform _Camera;


    public float TempsSmooth = 0.1f;
    float VelSmooth;

    float horitzontalInput;
    float verticalInput;

    Vector3 Direccio;

    private Text text;
    

    private Rigidbody rb;

    private bool contacteTerra = false;
    private bool correr = false;

    [SerializeField]
    private DispositiuLaser _dispositiu;
    private Outline _outlined=null;
    private bool _teDispositiu = false;

    [SerializeField] private AudioSource jumpSoundEffect;

    //Animacions personatge
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
      //  if (_Camera == null) _Camera = GameObjects.FindGameObjectWithTag("MainCamera");
        rb = GetComponent<Rigidbody>();
        text = GameObject.FindWithTag("Vel display").GetComponent<Text>();
        animator = GetComponent<Animator>();
        AgafarDispositiu();
    }

    //agafar els valors de control
    private void Control()
    {
        //dreta esquerra , valoe entre -1 i 1
        horitzontalInput = Input.GetAxisRaw("Horizontal");
        //endavant darere valor entre -1 i 1
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.LeftShift)) correr = true;
        else correr = false;

    }

    //Moviemnt, canviar direcció personatge aplicar velocitat i 
    private void Moviment()
    {
        Direccio = new Vector3(horitzontalInput, 0, verticalInput).normalized; //* orientacio.forward;
        //movDir.normalized;
        if (Direccio.magnitude >= 0.1)
        {
            float gir = Mathf.Atan2(Direccio.x, Direccio.z) * Mathf.Rad2Deg + _Camera.eulerAngles.y; //influeix la pos de camera
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, gir, ref VelSmooth, TempsSmooth); //gir del personatge suau
            transform.rotation = Quaternion.Euler(0, angle, 0);
            //efecte caminar
            if (!jumpSoundEffect.isPlaying)
            {
                jumpSoundEffect.Play();
            }
            //animacio de caminar
            animator.SetBool("IsWalking", true);
            Vector3 movDir = Quaternion.Euler(0,gir,0)* Vector3.forward;
            if(correr){
                rb.AddForce(movDir.normalized * velCorrer, ForceMode.VelocityChange);
            }
            else rb.AddForce(movDir.normalized * velCaminar, ForceMode.VelocityChange);
        }
        //deixa de caminar
        else
        {
            jumpSoundEffect.Stop();
            animator.SetBool("IsWalking", false);
        }
            


        //orientacio.transform = movDir;
    }

    //Actualitzacio independenment del frame-rete per a fisiques
    private void FixedUpdate()
    {
        Moviment();
    }

    //el personatge accelera molt, 
    private void ControlVelocitat()
    {
        Vector3 Vel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        Vector3 VelLimitada = Vel;
        if (Vel.magnitude > velCaminar && !correr)
        {
            VelLimitada = Vel.normalized * velCaminar; 
        }
        else if (Vel.magnitude > velCorrer && correr)
        {   
            VelLimitada = Vel.normalized * velCorrer;
        }
        rb.velocity = new Vector3(VelLimitada.x, rb.velocity.y, VelLimitada.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        contacteTerra = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        contacteTerra = false;
    }
   
    private void HighLightDisparable()
    {
        if (!_teDispositiu) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            Disparable target;
            if (hitInfo.collider.TryGetComponent<Disparable>(out target))
            {
                if (target.GetOutline() != _outlined && target.GetOutline()!=null)
                {
                    if (_outlined != null)
                    {
                        _outlined.enabled = false;
                    }
                    _outlined = target.GetOutline();
                    _outlined.enabled = true;
                }

            }
            else if(_outlined!=null)
            {
                _outlined.enabled = false;
                _outlined=null;
            }
        }
        else if (_outlined!=null)
        {
            _outlined.enabled = false;
            _outlined = null;
        }
    }
    private void DispararLaser()
    {
        if (!_teDispositiu) return;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                Disparable target;
                if(hitInfo.collider.TryGetComponent<Disparable>(out target))
                {
                    if (target._hasOutline) _dispositiu?.Shoot(target.Aparença());
                }
            }
        }
    }

    public void AgafarDispositiu()
    {
        _teDispositiu = true;
        _dispositiu.gameObject.SetActive(true);
        GetComponent<RigBuilder>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
       // _Camera.LookAt(rb);
        Control();
        ControlVelocitat();
        HighLightDisparable();
        DispararLaser();
        text.text = rb.velocity.ToString();
        if (Input.GetKeyDown(KeyCode.Space) && contacteTerra)
        {
            animator.SetTrigger("Jump");
            rb.AddForce(Vector3.up*forceSaltar, ForceMode.Impulse);
            contacteTerra = false;
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
