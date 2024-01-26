using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    protected static Material _materialOriginalEfecte = null;

    protected Material _instanciaMaterialEfecte = null;

    private float _tempsCanvi = 1.5f;
    private bool _tocatUltimFrame = false;
    private bool _interrupcioDesdeUltimCanviComplet = true;
    private bool _canviant = false;
    private float _tempsCancelacio = 0.05f;
    private float _tempsUltimHit = 0f;
    private float _tempsCanviant = 0f;
    private Vector3 _aparençaInicial;
    private Dictionary<Renderer, List<Material>> _savedMaterials = new Dictionary<Renderer, List<Material>>();
    private bool _hasToAddOutlineAfterChange = false;

    protected override void Awake()
    {
        base.Awake();
        if (_materialOriginalEfecte == null)
        {
            _materialOriginalEfecte=Resources.Load("EfecteLaser", typeof(Material)) as Material;
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
                _canviant = true;
                AfegirEfecteEncongirAObjecte();
                _aparençaInicial = _aparença.localScale;
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

    private void PosarEfecteAMesh(Renderer mesh)
    {
        List<Material> l = new List<Material>();
        mesh.GetMaterials(l);
        _savedMaterials[mesh] = l;
        List<Material> newList = new List<Material>();

        for (int i = 0; i < l.Count; i++)
        {
            _instanciaMaterialEfecte = new Material(_materialOriginalEfecte);
            newList.Add(_instanciaMaterialEfecte);
        }
        mesh.SetMaterials(newList);
    }


    private void AfegirEfecteEncongirAObjecte()
    {
        if (_outline)
        {
            _hasToAddOutlineAfterChange = _outline.enabled;
            _outline.enabled = false;
        }

        Renderer[] meshRenderers = _aparença.gameObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer meshRenderer in meshRenderers)
        {
            PosarEfecteAMesh(meshRenderer);
        }
    }

    private void TreureEfecteAMesh(Renderer mesh)
    {
        List<Material> l = _savedMaterials[mesh];
        mesh.SetMaterials(l);
        _instanciaMaterialEfecte = null;
    }

    private void TreureEfecteEncongirAObjecte()
    {


        Renderer[] meshRenderers = _aparença.gameObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer meshRenderer in meshRenderers)
        {
            TreureEfecteAMesh(meshRenderer);
        }
        if (_hasToAddOutlineAfterChange)
        {
            _outline.enabled = true;
        }
    }

    private void CanviarTamanyObjecte()
    {
        TreureEfecteEncongirAObjecte();
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
        TreureEfecteEncongirAObjecte();
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

    public override bool RemoveOutline()
    {
        if (!Canviant())
        {
            return base.RemoveOutline();
        }
        else
        {
            _hasToAddOutlineAfterChange = false;
            return true;
        }
    }

    public override bool AddOutline()
    {
        if (!Canviant())
        {
            return base.AddOutline();
        }
        else
        {
            _hasToAddOutlineAfterChange = true;
            return true;
        }
    }

}
