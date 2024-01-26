using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Disparable : MonoBehaviour
{
    [SerializeField]
    protected Outline _outline;
    [SerializeField]
    protected bool _outlineOnHover = true;

    [SerializeField]
    [Tooltip("Aqui va el transform d'un empty que sigui fill de l'objecte i representi la posicio on disparara el cientific, si es deixa buit llavors, _PuntDeAim és l'origen de _aparença")]
    protected Transform _PuntDeAim;



    protected virtual void Awake()
    {
        if (_outline!=null && _outlineOnHover) _outline.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public virtual bool AddOutline()
    {
        if (_outlineOnHover && _outline && _outline.fence)
        {
            _outline.enabled = true;
            return true;
        }
        else return false;
    }

    public virtual bool RemoveOutline()
    {
        if (_outlineOnHover && _outline && _outline.fence)
        {
            _outline.enabled = false;
            return true;
        }
        else return false;
    }

    public bool OutlinesOnHover()
    {
        return _outlineOnHover && _outline;
    }


    public virtual Transform AimPoint()
    {
        if (this._PuntDeAim == null) return this.transform;
        else return this._PuntDeAim;
    }

    public abstract bool TocatPelLaser(Laser laser, Vector3 pos_impacte);

}
