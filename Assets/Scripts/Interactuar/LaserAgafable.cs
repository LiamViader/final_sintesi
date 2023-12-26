using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAgafable : Interactuable
{
    public Disparable disparable;
    public ControlLaserQuiet _controlLaser;

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
        if (disparable.Petit() && !disparable.Canviant())
        {
            ControlPersonatge._instance.AgafarDispositiu();
            _controlLaser.FinalitzarLaser();
            Destroy(_controlLaser.gameObject);
        }
        else
        {
            UiControllerSingleton._instance._missatge.Mostrar("El dispositiu ha de ser més petit per poder agafar-lo", 1.5f);
        }
    }

}
