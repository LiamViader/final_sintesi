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
    [Tooltip("Aqui va el transform del objecte que anir� canviant de tamany durant l'animaci� de canvi")]
    protected Transform _aparen�a;

    [SerializeField]
    [Tooltip("Aqui va el transform del objecte que quedar� canviat de tamany un cop el canvi estigui completat")]
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
    private Vector3 _aparen�aInicial;
    private Dictionary<Renderer, List<Material>> _savedMaterials = new Dictionary<Renderer, List<Material>>();

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
                _aparen�a.localScale = Vector3.Lerp(_aparen�aInicial, _aparen�aInicial * grau, percentatgeCanvi);
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
                AfegirEfecteEncongirAObjecte();
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
            newList.Add(_materialEfecte);
        }
        mesh.SetMaterials(newList);
    }


    private void AfegirEfecteEncongirAObjecte()
    {

        Renderer[] meshRenderers = _aparen�a.gameObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer meshRenderer in meshRenderers)
        {
            PosarEfecteAMesh(meshRenderer);
        }
    }

    private void TreureEfecteAMesh(Renderer mesh)
    {
        List<Material> l = _savedMaterials[mesh];
        mesh.SetMaterials(l);
    }

    private void TreureEfecteEncongirAObjecte()
    {


        Renderer[] meshRenderers = _aparen�a.gameObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer meshRenderer in meshRenderers)
        {
            TreureEfecteAMesh(meshRenderer);
        }
    }

    private void CanviarTamanyObjecte()
    {
        TreureEfecteEncongirAObjecte();
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
        _interrupcioDesdeUltimCanviComplet = false;
    }

    private void ResetCanvi()
    {
        TreureEfecteEncongirAObjecte();
        _aparen�a.localScale = _aparen�aInicial;
        _canviant = false;
        _tempsCanviant = 0f;
        _tempsUltimHit = 0f;
    }

    public Transform Aparen�a()
    {
        return _aparen�a;
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
        if (this._PuntDeAim == null) return this._aparen�a;
        else return this._PuntDeAim;
    }

}
