using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiControllerSingleton : MonoBehaviour
{
    public PlayerInteractUI _playerInteract;
    public UiMissatgeError _missatgeError;
    public static UiControllerSingleton _instance;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
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
}
