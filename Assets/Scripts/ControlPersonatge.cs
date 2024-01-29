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

    public static ControlPersonatge _instance;

    public float forceSaltar = 5.0f;
    public float velCaminar = 7.0f;
    public float velCorrer = 12.0f;
 
    public Transform _Camera;
    public CapsuleCollider _collider;

    public float TempsSmooth = 0.1f;
    float VelSmooth;

    float horitzontalInput;
    float verticalInput;

    Vector3 Direccio;

    

    private Rigidbody rb;

    private bool correr = false;

    [SerializeField]
    private DispositiuLaser _dispositiu;
    private Disparable _outlined=null;
    private bool _teDispositiu = false;

    [SerializeField]
    private Encongible _tamany;

    [SerializeField] private AudioSource jumpSoundEffect;

    //Animacions personatge
    Animator animator;


    private bool _saltant=false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        _Camera=GestorHabsSingleton._instance.Camera().transform;
        //  if (_Camera == null) _Camera = GameObjects.FindGameObjectWithTag("MainCamera");
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
        if (_tamany.Petit())
        {
            velCaminar = 2;
            velCorrer = 3;
        }
        else
        {
            velCaminar = 4;
            velCorrer = 6;
        }
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

   
    private void HighLightDisparable()
    {
        if (!_teDispositiu) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, CameresHabitacio.getIgnoreMask()))
        {
            Disparable target;
            if (hitInfo.collider.TryGetComponent<Disparable>(out target) && target.OutlinesOnHover())
            {
                if (target != _outlined)
                {
                    if (_outlined != null)
                    {
                        _outlined.RemoveOutline();
                    }
                    target.AddOutline();
                    _outlined = target;
                }

            }
            else if (_outlined != null)
            {
                _outlined.RemoveOutline();
                _outlined = null;
            }
        }
        else if (_outlined!=null)
        {
            _outlined.RemoveOutline();
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
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, CameresHabitacio.getIgnoreMask()))
            {
                Disparable target;
                if(hitInfo.collider.TryGetComponent<Disparable>(out target))
                {
                    if(target.OutlinesOnHover()) _dispositiu?.Shoot(target.AimPoint());
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

        float factor = 1f;
        if (_tamany.Petit()) factor = 0.25f;

        if (Input.GetKeyDown(KeyCode.Space) && !_saltant)
        {
            Vector3 pos = new Vector3(_collider.center.x, _collider.center.y - _collider.height * 0.5f +(0.1f*factor), _collider.center.z);
            Vector3 posGlobal = transform.TransformPoint(pos);
            Ray ray = new Ray(posGlobal, Vector3.down);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 0.4f*factor))
            { // si esta tocant el terra
                animator.applyRootMotion = false;
                if (_tamany.Petit()) rb.AddForce(Vector3.up * 4000);
                else rb.AddForce(Vector3.up * 5000);

                _saltant = true;
            }
                
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
        else if(_saltant)
        {
            Vector3 pos = new Vector3(_collider.center.x, _collider.center.y - _collider.height * 0.5f + (0.1f * factor), _collider.center.z);
            Vector3 posGlobal = transform.TransformPoint(pos);
            Ray ray = new Ray(posGlobal, Vector3.down);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 0.4f*factor))
            {
                _saltant = false;
                animator.applyRootMotion = true;
            }
        }
       
    }

    public void PararMoviment()
    {
        animator.enabled = false;
        this.enabled = false;
    }

    public void ReanudarMoviment()
    {
        this.enabled = true;
        animator.enabled = true;
    }
}
