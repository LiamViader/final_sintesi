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
    int[] enlla� = new int[] { 0, 1, 2 };
    public Material[] textures;

    private void OnMouseDown()
    {
        if (gameObject.CompareTag("Interruptor"))
        {
            int interruptorIndex = int.Parse(gameObject.name.Substring("Interruptor".Length));
            interruptors[interruptorIndex] = !interruptors[interruptorIndex];

            Transform transformCubo = gameObject.GetComponent<Transform>();
            Vector3 nuevaRotacion = transformCubo.eulerAngles;
            nuevaRotacion.x = interruptors[interruptorIndex] ? 11.3f : -11.3f;
            transformCubo.eulerAngles = nuevaRotacion;

            if (interruptors.SequenceEqual(llums))
            {
                Resolt1 = true;
                Debug.Log("RESOLT 1");
            }
        }
        if (gameObject.CompareTag("Bloc"))
        {
            int blocIndex = int.Parse(gameObject.name.Substring("Bloc".Length));
            Renderer renderer = gameObject.GetComponent<Renderer>();
            if (bloc[blocIndex] == 2)
            {
                bloc[blocIndex] = 0;
                renderer.material = textures[0];
            }
            else if (bloc[blocIndex] == 0)
            {
                bloc[blocIndex] = 1;
                renderer.material = textures[1];
            }
            else
            {
                bloc[blocIndex] = 2;
                renderer.material = textures[2];
            }

            if (bloc.SequenceEqual(enlla�))
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