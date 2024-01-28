using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examinar : Interactuable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        PlaySound();
        UiControllerSingleton._instance._missatge.Mostrar("És massa gran per sortir", 1.5f);
    }
}
