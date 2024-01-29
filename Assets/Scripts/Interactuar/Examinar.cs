using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Examinar : Interactuable
{
    public Encongible encongible;

    public UnityEvent PotObrir;

    private Collider col; 

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
            Destroy(gameObject);
                        
        }
        else
        {
            UiControllerSingleton._instance._missatge.Mostrar("Ha de ser mes petit per poder agafar-lo", 1.5f);
            PlaySoundIncorrecte();
        }
    }
}
