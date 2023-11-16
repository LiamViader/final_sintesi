using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MirallDisparable : Disparable
{
    private Dictionary<Laser, KeyValuePair<Laser,bool>> _lasers; //mapa de laser entrada/sortida, bool"tocat ultim frame s/n"
    private const int _maxLasers = 6;
    [SerializeField]
    private GameObject _laserPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        foreach (var clave in _lasers.Keys.ToList())
        {
            // Verificar si el objeto asociado a la clave aún existe
            if (clave != null)
            {
                // Acceder al componente o realizar acciones adicionales
                // ...
                if (!_lasers[clave].Value) {
                    _lasers[clave].Key.FinishImmediately();
                    _lasers.Remove(clave);
                }
                else
                {
                    _lasers[clave] = new KeyValuePair<Laser, bool>(_lasers[clave].Key, false);
                }
            }
            else
            {
                // El objeto asociado a la clave ha sido destruido, puedes manejarlo aquí
                _lasers.Remove(clave); // O realizar otras acciones
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
        if (_lasers.Count < 6)
        {
            GameObject instance = Instantiate(_laserPrefab);
            Laser newLaser = instance.GetComponent<Laser>();
            Vector3 dir = ComputeNewDirection(laser);
            Vector3 norm = Vector3.Normalize(dir);
            newLaser.Init(pos_impacte, norm, -1,laser); 
            _lasers[laser] = new KeyValuePair<Laser, bool>(newLaser, true);
        }
    }

    private void UpdateLaser(Laser laser_in, Laser laser_out, Vector3 pos_impacte)
    {
        laser_out.UpdateDirection(ComputeNewDirection(laser_in), pos_impacte);
    }

    private Vector3 ComputeNewDirection(Laser laser)
    {
        return Vector3.Reflect(laser.Direction(), transform.forward);
    }
}
