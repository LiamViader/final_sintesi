using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.Events;

public class MinijocInteractuable : Interactuable
{
    
    [SerializeField] private bool _teAparença;
    [SerializeField] private CameresHabitacio _cameresMinijoc;
    [SerializeField] private MeshRenderer _aparençaMinijocNoObert; 
    [SerializeField] private GameObject _minijoc; 
    private CameresHabitacio _last_hab;
    private bool _interactuant = false;
    private string _baseInteractText;

    public UnityEvent abilita, desabilita;

    void Start()
    {

    }

    public override void Interact() {
        if (!_interactuant)
        {
            _last_hab = GestorHabsSingleton._instance.ActiveHab();//guardo el gestor de cameres anteriors per a poder tornar
            GestorHabsSingleton._instance.CanviarHab(_cameresMinijoc, new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.HardOut, 2));
            if (_teAparença)
            {
                _aparençaMinijocNoObert.enabled = false;
                _minijoc.SetActive(true);
            }
            _interactuant = true;
            ControlPersonatge._instance.enabled = false;
            _baseInteractText = interactText;
            interactText = "Deixar d'interactuar";
            abilita.Invoke();
        }
        else
        { // es prem interactuar mentres s'est� interactuant, �s a dir es vol deixar d'interactuar
            AcabarInteractuar();
            desabilita.Invoke();
        }

    }

    public void AcabarInteractuar()
    {
        interactText = _baseInteractText;
        GestorHabsSingleton._instance.CanviarHab(_last_hab, new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.HardIn, 2));
        if (_teAparença)
        {
            _aparençaMinijocNoObert.enabled = true;
            _minijoc.SetActive(false);
        }
        _interactuant = false;
        ControlPersonatge._instance.enabled = true;
    }

    public bool getInteractuar(){
        return _interactuant;
    }
}
