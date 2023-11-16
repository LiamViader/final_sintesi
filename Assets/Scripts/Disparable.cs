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
    [Tooltip("Aqui va el transform del objecte que anirà canviant de tamany durant l'animació de canvi")]
    private Transform _aparença;

    [SerializeField]
    [Tooltip("Aqui va el transform del objecte que quedarà canviat de tamany un cop el canvi estigui completat")]
    private Transform _canviReal;
    private float _tempsCanvi = 3f;
    private bool _canviant = false;
    private float _tempsCancelacio = 0.1f;
    private float _tempsUltimHit = 0f;
    private float _tempsCanviant = 0f;
    private Vector3 _aparençaInicial;


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
                _aparença.localScale = Vector3.Lerp(_aparençaInicial, _aparençaInicial * grau, percentatgeCanvi);
            }
        }

    }

    //retorna 1 si amb aquest hit ha acabat de canviar de tamany, 0 si no;
    public bool TocatPelLaser(Laser laser)
    {
        if (!_canviant)
        {
            _aparençaInicial = _aparença.localScale;
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
        _aparença.localScale = _aparençaInicial;
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
        _aparença.localScale = _aparençaInicial;
        _canviant = false;
        _tempsCanviant = 0f;
        _tempsUltimHit = 0f;
    }

    public Transform Aparença()
    {
        return _aparença;
    }

    public Outline GetOutline()
    {
        return _outline;
    }
}
