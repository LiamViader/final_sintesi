using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GestorHabsSingleton : MonoBehaviour
{
    public static GestorHabsSingleton _instance;
    private CameresHabitacio _HabAct;
    private CinemachineVirtualCamera _activeCam;
    [SerializeField]
    private Camera _cam;
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
            _activeCam=_HabAct?.SeguentCam();
        }
    }

    public void CanviarHab(CameresHabitacio hab)
    {
        _HabAct?.Desactivar();
        _HabAct = hab;
        _activeCam=_HabAct.Activar();
    }

    public Camera Camera()
    {
        return _cam;
    }

    public CinemachineVirtualCamera ActiveCamera()
    {
        return _activeCam;
    }

    public CameresHabitacio ActiveHab()
    {
        return _HabAct;
    }
}
