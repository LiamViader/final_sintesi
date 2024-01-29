using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicInteractuar : Interactuable
{
    public UnityEvent PotObrir;
    public override void Interact(){
            PlaySound();
            PotObrir.Invoke();
            Destroy(gameObject);
    }
}
