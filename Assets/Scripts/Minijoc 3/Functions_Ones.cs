using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Functions_Ones : Observer
{
    const int punts = 120;
    const float XIni = -1;
    const float XFi = 1;
    public LineRenderer Ona;
    private float Amplitud=0.03f;
    private float Fase=0.5f;

    public bool Dinamic = false;

    // Start is called before the first frame update
    void Start()
    {
        Ona = GetComponent<LineRenderer>();
        Ona.widthMultiplier = 0.4f;
        if (!Dinamic){
            Valors_random();
        }
        Crea_ona(); 
    }

    // Update is called once per frame
    void Update()
    {
         Crea_ona();
    }

    void Crea_ona(){
        float Inici = XIni;
        float Tau = 2*Mathf.PI;
        float Final = XFi;
        Ona.positionCount = punts;
        for (int PuntActual = 0; PuntActual < punts; PuntActual++ ){
            float posicio = (float)PuntActual/(punts-1);
            float x = Mathf.Lerp(Inici,Final,posicio);
            float y = Amplitud* Mathf.Sin(x*Tau*Fase)+transform.position.y;
            Ona.SetPosition(PuntActual,new Vector3(x,y,-0.7F));
        }
    }

    void Valors_random(){
        Amplitud = Random.Range(0.03f,0.3f);
        Fase = Random.Range(0.5f,5f);
    }

    public void ActualitzaAmplitud(float value){
        //Debug.Log("Rep" + value);
        Amplitud =value * 0.00075f + 0.03f;
        Crea_ona();
    }

     public void ActualitzaFase(float value){
        //Debug.Log("Rep" + value);
        Fase =value * 0.0125f + 0.5f;
        Crea_ona();
    }

    public bool Igual(float A, float F){ //A i F son els valors de la dinamica
        bool same = false;
        if (A < Amplitud+0.01 && A > Amplitud-0.01){
            if (F < Fase+0.1 && F > Fase-0.1){
                same = true;
            }
        }
        return same; 
    }

    public void GetDades(float A, float F){
        A = Amplitud;
        F = Fase;
    }

}

