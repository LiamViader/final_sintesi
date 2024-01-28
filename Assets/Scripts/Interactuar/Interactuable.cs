using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactuable : MonoBehaviour
{
    [SerializeField] protected string interactText;
    public static AudioSource _audioInteract=null;

    private void Awake()
    {
        if (_audioInteract==null)
        {
            GameObject objecte= Instantiate(Resources.Load<GameObject>("AudioInteract"),transform);
            _audioInteract = objecte.GetComponent<AudioSource>();
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
        _audioInteract.Play();
    }

    public abstract void Interact();

    public string returnInteractText()
    {
        return interactText;
    }
    
}
