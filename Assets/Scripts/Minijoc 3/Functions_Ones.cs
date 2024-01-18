using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions_Ones : MonoBehaviour
{
    const int punts = 120;
    const float XIni = -1;
    const float XFi = 1;
    public LineRenderer Ona;
    public float Periode=4;
    public float Fase=1;

    public bool Dinamic = false;

    public float alçada = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        Ona = GetComponent<LineRenderer>();
        Ona.widthMultiplier = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Dinamic){
            Crea_ona(); 
        }
    }

    void Crea_ona(){
        float Inici = XIni;
        float Tau = 2*Mathf.PI;
        float Final = XFi;
        Ona.positionCount = punts;
        for (int PuntActual = 0; PuntActual < punts; PuntActual++ ){
            float posicio = (float)PuntActual/(punts-1);
            float x = Mathf.Lerp(Inici,Final,posicio);
            float y = 0.2F* Mathf.Sin(x*Tau*Fase+Periode)+alçada;
            Ona.SetPosition(PuntActual,new Vector3(x,y,-0.7F));
        }
    }
}
