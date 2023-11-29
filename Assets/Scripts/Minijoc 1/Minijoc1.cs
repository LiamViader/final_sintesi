using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Minijoc1 : MonoBehaviour
{
    bool Resolt = false;
    static bool Resolt1 = false;
    static bool Resolt2 = false;
    static bool[] interruptors = new bool[] { false, false, false, false };
    bool[] llums = new bool[] { true, false, true, true };
    static int[] bloc = new int[] { 0, 0, 0 };
    int[] enllaç = new int[] { 0, 1, 2 };

    private void OnMouseDown()
    {
        if (gameObject.CompareTag("Interruptor"))
        {
            int interruptorIndex = int.Parse(gameObject.name.Substring("Interruptor".Length));
            interruptors[interruptorIndex] = !interruptors[interruptorIndex];

            Transform transformCubo = gameObject.GetComponent<Transform>();
            Vector3 nuevaPosicion = transformCubo.position;
            nuevaPosicion.y = interruptors[interruptorIndex] ? 0.28f : 0.25f;
            transformCubo.position = nuevaPosicion;

            if (interruptors.SequenceEqual(llums))
            {
                Resolt1 = true;
                Debug.Log("RESOLT 1");
            }
        }
        if (gameObject.CompareTag("Bloc"))
        {
            int blocIndex = int.Parse(gameObject.name.Substring("Bloc".Length));
            if (bloc[blocIndex] == 2)
            {
                bloc[blocIndex] = 0;
                //CANVIAR ASSETS AQUÍ
            }
            else
            {
                bloc[blocIndex] = bloc[blocIndex]+1;
                //CANVIAR ASSETS AQUÍ
            }

            if (bloc.SequenceEqual(enllaç))
            {
                Resolt2 = true;
                Debug.Log("RESOLT 2");
            }
        }
        if (Resolt1 && Resolt2)
        {
            Resolt = true;
            Debug.Log("RESOLT MINIJOC");
        }
    }
}