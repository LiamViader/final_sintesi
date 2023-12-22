using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MirallDisparable : Disparable
{
    private Dictionary<Laser, KeyValuePair<Laser,bool>> _lasers= new Dictionary<Laser, KeyValuePair<Laser, bool>>(); //mapa de laser entrada/sortida, bool"tocat ultim frame s/n"
    private const int _maxLasers = 6;
    [SerializeField]
    private GameObject _laserPrefab;
    public string _name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {

        foreach (Laser clave in _lasers.Keys.ToList())
        {

            if (_lasers.ContainsKey(clave))
            {
                if (!_lasers[clave].Value)
                { //si no ha sigut tocat l'ultim frame pel laser clave

                    if (_lasers[clave].Key != null)
                    {
                        _lasers[clave].Key.Unsubscribe(OnLaserImageDestroy);
                        _lasers[clave].Key.FinishImmediately();
                    }
                    if (_lasers.ContainsKey(clave))
                    {
                        _lasers.Remove(clave);
                        clave.Unsubscribe(OnLaserSourceDestroy);
                    }
                }
                else
                {
                    _lasers[clave] = new KeyValuePair<Laser, bool>(_lasers[clave].Key, false);
                }
            }
            else
            {
            }
        }

    }

    public override bool TocatPelLaser(Laser laser, Vector3 pos_impacte)
    {
        if (!_lasers.ContainsKey(laser))
        {
            AddNewLaser(laser, pos_impacte);
        }
        else
        {
            _lasers[laser] = new KeyValuePair<Laser, bool>(_lasers[laser].Key, true);
            UpdateLaser(laser, _lasers[laser].Key, pos_impacte);
        }
        return false;
    }

    private void AddNewLaser(Laser laser, Vector3 pos_impacte)
    {
        if (_lasers.Count < _maxLasers)
        {
            GameObject instance = Instantiate(_laserPrefab);
            Laser newLaser = instance.GetComponent<Laser>();
            Vector3 dir = ComputeNewDirection(laser);
            Vector3 norm = Vector3.Normalize(dir);
            laser.Subscribe(OnLaserSourceDestroy);
            newLaser.Init(pos_impacte, norm, -1,laser.Width(),laser.DestruitControlatPerDisparable());
            newLaser.Subscribe(OnLaserImageDestroy);
            _lasers[laser] = new KeyValuePair<Laser, bool>(newLaser, true);
        }
    }

    private void UpdateLaser(Laser laser_in, Laser laser_out, Vector3 pos_impacte)
    {
        laser_out.UpdateDirection(ComputeNewDirection(laser_in), pos_impacte, laser_in.Width());
    }

    private Vector3 ComputeNewDirection(Laser laser)
    {
        return Vector3.Reflect(laser.Direction(), transform.forward);
    }

    private void OnLaserSourceDestroy(Laser laser)
    {
        laser.Unsubscribe(OnLaserSourceDestroy);
        if (_lasers.ContainsKey(laser))
        {
            Laser image = _lasers[laser].Key;
            image.Unsubscribe(OnLaserImageDestroy);
            image.FinishImmediately();
            _lasers.Remove(laser);
        }
    }

    private void OnLaserImageDestroy(Laser laser)
    {
        laser.Unsubscribe(OnLaserImageDestroy);
        if(_lasers.ContainsValue(new KeyValuePair<Laser, bool>(laser, true)) || _lasers.ContainsValue(new KeyValuePair<Laser, bool>(laser, true)))
        {
            foreach (Laser source in _lasers.Keys.ToList())
            {

                if (_lasers.ContainsKey(source))
                {
                    if (_lasers[source].Key == laser)
                    {
                        source.Unsubscribe(OnLaserSourceDestroy);
                        _lasers.Remove(source);
                        source.FinishImmediately();
                    }
                }

            }
        }
    }
}
