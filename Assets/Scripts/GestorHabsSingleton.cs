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
    private CinemachineBrain _brain;
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
        _brain = _cam.GetComponent<CinemachineBrain>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _brain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, 1);
            _activeCam =_HabAct?.SeguentCam();
        }
    }

    public void CanviarHab(CameresHabitacio hab, CinemachineBlendDefinition def)
    {
        _brain.m_DefaultBlend = def;
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
