using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Functions_Ones : Observer
{
    const int punts = 120;
    const float XIni = -0.7F;
    const float XFi = 1.43F;
    private LineRenderer Ona;

    const float Ma = 0.2f;
    const float Mf = 4f;
    const float ma = 0.04f;
    const float mf = 0.4f;
    private float NewAmplitud;
    private float NewFase;

    private float Width = 0.4f;

    public bool Dinamic = false;

    private bool Acabat = false;

    private GameObject radio;

    // Start is called before the first frame update
    void Start()
    {

        radio = this.transform.parent.gameObject;
        //Debug.Log(radio.name);
        Ona = GetComponent<LineRenderer>();
        Ona.widthMultiplier = Width;

        if (!Dinamic){
            Valors_random();
        }
        
        else{
            NewAmplitud = ma;
            NewFase = mf;
        }
        Crea_ona(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (!Acabat){
            Crea_ona();
        }
    }

    void Crea_ona(){
        float Tau = 2*Mathf.PI; 
        float z = transform.position.z;
       
        Ona.positionCount = punts;
        Ona.widthMultiplier = Width*radio.transform.localScale.y;
        for (int PuntActual = 0; PuntActual < punts; PuntActual++ ){
            float posicio = (float)PuntActual/(punts-1);
            float x = Mathf.Lerp(XIni,XFi,posicio);
            float y = NewAmplitud* Mathf.Sin(x*Tau*NewFase);
            Vector3 pos = new Vector3(x,y,z);
            Ona.SetPosition(PuntActual,pos);
        }
    }

    void Valors_random(){
        NewAmplitud = Random.Range(ma,Ma);
        NewFase = Random.Range(mf,Mf);
    }

    public void ActualitzaAmplitud(float value){
        //Debug.Log("Rep" + value);
        NewAmplitud = value * (Ma/360f)*0.9f + ma;
        Crea_ona();
    }

     public void ActualitzaFase(float value){
        //Debug.Log("Rep" + value);
        NewFase =value * (Mf/360f)*0.9f + mf;
        Crea_ona();
    }

    public bool Igual(float A, float F){ //A i F son els valors de la dinamica
        bool same = false;
        if (A < NewAmplitud+0.02 && A > NewAmplitud-0.02){
            if (F < NewFase+0.1 && F > NewFase-0.1){
                same = true;
            }
        }
        return same; 
    }

    public void GetDades(out float A, out float F){
        A = NewAmplitud;
        F = NewFase;
    }

    public void OnAcabat(){
        Acabat = true;
    }
}

