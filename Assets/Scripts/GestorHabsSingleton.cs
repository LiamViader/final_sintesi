using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorHabsSingleton : MonoBehaviour
{
    public static GestorHabsSingleton _instance;
    private CameresHabitacio _HabAct;
    private void Awake()
    {
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
        if (Input.GetKeyDown(KeyCode.C))
        {
            _HabAct?.SeguentCam();
        }
    }

    public void CanviarHab(CameresHabitacio hab)
    {
        _HabAct?.Desactivar();
        _HabAct = hab;
        _HabAct.Activar();
    }
}
