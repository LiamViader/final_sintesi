using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparable : MonoBehaviour
{
    [SerializeField]
    private bool _petit = false;
    [SerializeField]
    private float _coefTamany = 0.5f;
    [SerializeField]
    private Outline _outline;

    [SerializeField]
    [Tooltip("Aqui va el transform del objecte que anir� canviant de tamany durant l'animaci� de canvi")]
    private Transform _aparen�a;

    [SerializeField]
    [Tooltip("Aqui va el transform del objecte que quedar� canviat de tamany un cop el canvi estigui completat")]
    private Transform _canviReal;
    private float _tempsCanvi = 3f;
    private bool _canviant = false;
    private float _tempsCancelacio = 0.1f;
    private float _tempsUltimHit = 0f;
    private float _tempsCanviant = 0f;
    private Vector3 _aparen�aInicial;


    private void Awake()
    {
        _outline.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_canviant)
        {
            _tempsUltimHit += Time.fixedDeltaTime;
            _tempsCanviant += Time.fixedDeltaTime;

            if (_tempsUltimHit > _tempsCancelacio) ResetCanvi();
            else
            {
                float percentatgeCanvi = _tempsCanviant / _tempsCanvi;
                float grau = _coefTamany;
                if (_petit)
                {
                    grau = 1 / grau;
                }
                _aparen�a.localScale = Vector3.Lerp(_aparen�aInicial, _aparen�aInicial * grau, percentatgeCanvi);
            }
        }

    }

    //retorna 1 si amb aquest hit ha acabat de canviar de tamany, 0 si no;
    public bool TocatPelLaser(Laser laser)
    {
        if (!_canviant)
        {
            _aparen�aInicial = _aparen�a.localScale;
            _canviant = true;
            _tempsCanviant = 0f;
        }
        _tempsUltimHit = 0f;
        if (_tempsCanviant >= _tempsCanvi)
        {
            CanviarTamanyObjecte();
            return true;
        }
        return false;
    }

    private void CanviarTamanyObjecte()
    {
        _canviant = false;
        _aparen�a.localScale = _aparen�aInicial;
        if (!_petit) {
            _canviReal.transform.localScale = _canviReal.transform.localScale * _coefTamany;
            _petit = true;
        }
        else
        {
            _canviReal.transform.localScale = _canviReal.transform.localScale * 1/_coefTamany;
            _petit = false;
        }
    }

    private void ResetCanvi()
    {
        _aparen�a.localScale = _aparen�aInicial;
        _canviant = false;
        _tempsCanviant = 0f;
        _tempsUltimHit = 0f;
    }

    public Transform Aparen�a()
    {
        return _aparen�a;
    }

    public Outline GetOutline()
    {
        return _outline;
    }
}
