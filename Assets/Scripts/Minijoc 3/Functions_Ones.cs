using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions_Ones : MonoBehaviour
{
    const int punts = 120;
    const float XIni = -1;
    const float XFi = 1;
    public LineRenderer Ona;
    public float Amplitud=0.3f;
    public float Fase=1;

    public bool Dinamic = false;

    private float alçada = 0.4f;
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
            if (Dinamic) y += alçada;
            Ona.SetPosition(PuntActual,new Vector3(x,y,-0.7F));
        }
    }

    void Valors_random(){
        Amplitud = Random.Range(0.03f,0.3f);
        Fase = Random.Range(0.5f,5f);
    }

    private void ActualitzaAmplitud(float value){
        Amplitud =value * 0.00075f + 0.03f;
    }

     private void ActualitzaFase(float value){
        Fase =value * 0.0125f + 0.5f;
    }
}
