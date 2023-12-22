using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAgafable : MeshInteractuable
{
    public ControlPersonatge _personatge;
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
            _personatge.AgafarDispositiu();
            _controlLaser.FinalitzarLaser();
            Destroy(_controlLaser.gameObject);
        }
        else
        {
            UiControllerSingleton._instance._missatgeError.Mostrar("El dispositiu és massa gran per a ser agafat", 1.5f);
        }
    }

}
