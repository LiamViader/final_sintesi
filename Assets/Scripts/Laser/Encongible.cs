using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encongible : Disparable
{
    [SerializeField]
    protected bool _petit = false;
    [SerializeField]
    protected float _coefTamany = 0.5f;


    [SerializeField]
    [Tooltip("Aqui va el transform del objecte que anirà canviant de tamany durant l'animació de canvi")]
    protected Transform _aparença;

    [SerializeField]
    [Tooltip("Aqui va el transform del objecte que quedarà canviat de tamany un cop el canvi estigui completat")]
    protected Transform _canviReal;

    [SerializeField]
    protected static Material _materialEfecte = null;


    private float _tempsCanvi = 1.5f;
    private bool _tocatUltimFrame = false;
    private bool _interrupcioDesdeUltimCanviComplet = true;
    private bool _canviant = false;
    private float _tempsCancelacio = 0.05f;
    private float _tempsUltimHit = 0f;
    private float _tempsCanviant = 0f;
    private Vector3 _aparençaInicial;

    protected override void Awake()
    {
        base.Awake();
        if (_materialEfecte == null)
        {
            _materialEfecte=Resources.Load("EfecteLaser", typeof(Material)) as Material;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if (!_tocatUltimFrame) _interrupcioDesdeUltimCanviComplet = true;
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
        _tocatUltimFrame = false;
    }

    //retorna 1 si amb aquest hit ha acabat de canviar de tamany, 0 si no;
    public override bool TocatPelLaser(Laser laser,Vector3 pos_impacte)
    {
        _tocatUltimFrame = true;
        if (_interrupcioDesdeUltimCanviComplet)
        {
            if (!_canviant)
            {
                AfegirEfecteEncongir();
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
        }
        return false;
    }

    private void AfegirEfecteEncongir()
    {
        if (_aparença.gameObject.TryGetComponent<MeshRenderer>(out MeshRenderer mesh))
        {
            List < Material > l= new List<Material>();
            mesh.GetMaterials(l);
            l.Add(_materialEfecte);
            mesh.SetMaterials(l);
        }


        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            List<Material> l = new List<Material>();
            meshRenderer.GetMaterials(l);
            l.Add(_materialEfecte);
            meshRenderer.SetMaterials(l);
        }
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
        _interrupcioDesdeUltimCanviComplet = false;
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


    public bool Petit()
    {
        return _petit;
    }

    public bool Canviant()
    {
        return _canviant;
    }

    public override Transform AimPoint()
    {
        if (this._PuntDeAim == null) return this._aparença;
        else return this._PuntDeAim;
    }

}
