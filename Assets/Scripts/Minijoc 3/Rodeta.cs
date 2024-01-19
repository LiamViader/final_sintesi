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

    private Vector3 PuntClic;
    private Collider col;
    private float AngleOff;
    private Camera cam;
    private Vector3 screenPos;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        col = GetComponent<Collider>();
        

    }

    // Update is called once per frame
  

    void Update()
    {
        Vector3 mousePos = cam.transform.position;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit)){
            mousePos = raycastHit.point; 
            if (Input.GetMouseButtonDown(0)){   
                if (col == raycastHit.collider.gameObject.GetComponent<Collider>()){
                    screenPos = cam.WorldToScreenPoint(transform.position);
                    Vector3 vec3 = Input.mousePosition - screenPos;
                    
                    AngleOff = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(vec3.y, vec3.x)) * Mathf.Rad2Deg;
                    //Valors rot al fer clic
                }
            }
            if (Input.GetMouseButton(0)){ 
                if (col == raycastHit.collider.gameObject.GetComponent<Collider>()){
                    Vector3 vec3 = Input.mousePosition - screenPos;
                    float angle = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;
                    transform.eulerAngles = new Vector3(0,0, angle + AngleOff);
                    actualitza.Invoke(transform.eulerAngles.z);
                }
            }
            if(Input.GetMouseButtonUp(0)){
                
                Debug.Log(transform.eulerAngles.z);
            }
            
        }

    }
 
}
