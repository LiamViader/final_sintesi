using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MyEvent: UnityEvent<float>{}

public class Rodeta : MonoBehaviour
{

    //public List<Observer>Observers;

    public MyEvent actualitza;
     [SerializeField] 


    private Vector3 PuntClic;
    private Collider col;
    private float AngleOff;

    private float TotRot;
    private Camera cam;
    private Vector3 screenPos;

    private Vector3 PosClic;

    private bool Acabat = false;

    public UnityEvent clic;

    private float y , a;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        col = GetComponent<Collider>();
        col.enabled = false;
    }

    // Update is called once per frame
  

    void Update()
    {
        if (!Acabat){
          Vector3 mousePos = cam.transform.position;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit)){
                mousePos = raycastHit.point; 
                if (Input.GetMouseButtonDown(0)){   
                    if (col == raycastHit.collider.gameObject.GetComponent<Collider>()){
                        screenPos = cam.WorldToScreenPoint(transform.position);
                        PosClic = Input.mousePosition - screenPos;

                        y = Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;
                        a = Mathf.Atan2(PosClic.y, PosClic.x) * Mathf.Rad2Deg;
                        AngleOff = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(PosClic.y, PosClic.x)) * Mathf.Rad2Deg;
                        //AngleIni = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(vec3.y, vec3.x)) * Mathf.Rad2Deg ;
                        clic.Invoke();
                    }
                }
                if (Input.GetMouseButton(0)){ 
                    if (col == raycastHit.collider.gameObject.GetComponent<Collider>()){
                        Vector3 vec3 = Input.mousePosition - screenPos;
                        float angle = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;
                        
                        //float 
                        //Debug.Log("Right : "+y+" Mouse : "+a);
                        //Debug.Log("Inicial: "+AngleIni + "Angle: "+angle);
                            transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y, angle + AngleOff);
                            actualitza.Invoke(transform.eulerAngles.z);
                      
                    }
                }
            }  
        }
    }
    
    public void OnAcabat(){
        Acabat = true;
    }

}

