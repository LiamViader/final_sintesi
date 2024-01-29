using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Events;

public class Examinar : Interactuable
{
    public Encongible encongible;
    public ControlLaserQuiet _controlLaser;

    public UnityEvent PotObrir;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
         if (encongible.Petit() && !encongible.Canviant())
        {
            PlaySound();
            PotObrir.Invoke();
            _controlLaser.FinalitzarLaser();
            
        }
        else
        {
            UiControllerSingleton._instance._missatge.Mostrar("El dispositiu ha de ser m�s petit per poder agafar-lo", 1.5f);
            PlaySoundIncorrecte();
        }
    }
}
