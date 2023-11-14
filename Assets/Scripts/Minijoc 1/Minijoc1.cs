using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minijoc1 : MonoBehaviour
{
    bool[] interruptors = new bool[] { false, false, false, false };
    private void OnMouseDown()
    {
        if (gameObject.CompareTag("Interruptor"))
        {
            if (gameObject.name == "Interruptor0")
            {
                interruptors[0] = !interruptors[0];
                Transform transformCubo = GetComponent<Transform>();
                Vector3 nuevaPosicion = transformCubo.position;
                if(interruptors[0] == true)
                {
                    nuevaPosicion.y = 0.28f;
                }
                else
                {
                    nuevaPosicion.y = 0.25f;
                }
                transformCubo.position = nuevaPosicion;
            }
            else if (gameObject.name == "Interruptor1")
            {
                interruptors[1] = !interruptors[1];
                Transform transformCubo = GetComponent<Transform>();
                Vector3 nuevaPosicion = transformCubo.position;
                if (interruptors[1] == true)
                {
                    nuevaPosicion.y = 0.28f;
                }
                else
                {
                    nuevaPosicion.y = 0.25f;
                }
                transformCubo.position = nuevaPosicion;
            }
            else if (gameObject.name == "Interruptor2")
            {
                interruptors[2] = !interruptors[2];
                Transform transformCubo = GetComponent<Transform>();
                Vector3 nuevaPosicion = transformCubo.position;
                if (interruptors[2] == true)
                {
                    nuevaPosicion.y = 0.28f;
                }
                else
                {
                    nuevaPosicion.y = 0.25f;
                }
                transformCubo.position = nuevaPosicion;
            }
            else
            {
                interruptors[3] = !interruptors[3];
                Transform transformCubo = GetComponent<Transform>();
                Vector3 nuevaPosicion = transformCubo.position;
                if (interruptors[3] == true)
                {
                    nuevaPosicion.y = 0.28f;
                }
                else
                {
                    nuevaPosicion.y = 0.25f;
                }
                transformCubo.position = nuevaPosicion;
            }
        }
    }
}
