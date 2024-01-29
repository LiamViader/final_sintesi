using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactuable : MonoBehaviour
{
    [SerializeField] protected string interactText;
    public static AudioSource _audioInteract=null;
    public static AudioSource _audioIncorrecte = null;

    private void Awake()
    {
        if (_audioInteract==null)
        {
            GameObject objecte= Instantiate(Resources.Load<GameObject>("AudioInteract"),transform);
            _audioInteract = objecte.GetComponent<AudioSource>();
        }
        if (_audioIncorrecte == null)
        {
            GameObject objecte2 = Instantiate(Resources.Load<GameObject>("AudioInteractIncorrecte"), transform);
            _audioIncorrecte = objecte2.GetComponent<AudioSource>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound()
    {
        if(_audioInteract == null)
        {
            GameObject objecte = Instantiate(Resources.Load<GameObject>("AudioInteract"), transform);
            _audioInteract = objecte.GetComponent<AudioSource>();
        }
        _audioInteract.Play();
    }

    public void PlaySoundIncorrecte()
    {
        if (_audioIncorrecte == null)
        {
            GameObject objecte2 = Instantiate(Resources.Load<GameObject>("AudioInteractIncorrecte"), transform);
            _audioIncorrecte = objecte2.GetComponent<AudioSource>();
        }
        _audioIncorrecte.Play();
    }

    public abstract void Interact();

    public string returnInteractText()
    {
        return interactText;
    }
    
}
