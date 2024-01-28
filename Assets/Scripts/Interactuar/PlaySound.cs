using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : Interactuable
{
    [SerializeField]
    private AudioSource _audio;
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
        if (!_audio.isPlaying) _audio.Play();
        else
        {
            UiControllerSingleton._instance._missatge.Mostrar("Ja s'està reproduint", 1.5f);
            PlaySoundIncorrecte();
        }
    }
}
