using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotAlwaysInteractuable : MinijocInteractuable
{
    [SerializeField]
    protected bool _available=true;
    [SerializeField]
    protected string _notAvailableMessage = "";
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
        if (!_available)
        {
            PlaySoundIncorrecte();
            UiControllerSingleton._instance._missatge.Mostrar(_notAvailableMessage, 1.5f);
        }
        else
        {
            base.Interact();
        }
    }

    public void MakeAvailable()
    {
        _available = true;
    }

    public void MakeUnavailable()
    {
        _available = true;
    }

    public void ChangeAvailableText(string text)
    {
        _notAvailableMessage = text;
    }
}
