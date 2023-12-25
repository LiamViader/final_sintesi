using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class MeshInteractuable : InteractBase
{
    [SerializeField] private CameresHabitacio _cameresMinijoc;
    [SerializeField] private MeshRenderer _aparençaMinijoc; 
    [SerializeField] private GameObject _minijoc; 
    private CameresHabitacio _last_hab;
    private bool _interactuant = false;
    private string _baseInteractText;

    void Start()
    {

    }

    public override void Interact() {
        if (!_interactuant)
        {
            _last_hab = GestorHabsSingleton._instance.ActiveHab();//guardo el gestor de cameres anteriors per a poder tornar
            GestorHabsSingleton._instance.CanviarHab(_cameresMinijoc, new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.HardOut, 2));
            _aparençaMinijoc.enabled = false;
            _minijoc.SetActive(true);
            _interactuant = true;
            ControlPersonatge._instance.enabled = false;
            _baseInteractText = interactText;
            interactText = "Deixar d'interactuar";
        }
        else
        { // es prem interactuar mentres s'està interactuant, és a dir es vol deixar d'interactuar
            AcabarInteractuar();
        }

    }

    public void AcabarInteractuar()
    {
        interactText = _baseInteractText;
        GestorHabsSingleton._instance.CanviarHab(_last_hab, new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.HardIn, 2));
        _aparençaMinijoc.enabled = true;
        _minijoc.SetActive(false);
        _interactuant = false;
        ControlPersonatge._instance.enabled = true;
    }
}
