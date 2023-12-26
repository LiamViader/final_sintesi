using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDefinit : Interactuable
{
    public Transform _objecteQueRota;
    public List<Vector3> _rotacions;
    private int act = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(_rotacions.Count>0) _objecteQueRota.transform.rotation = Quaternion.Euler(_rotacions[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        if (_rotacions.Count > 0) SeguentPos();
    }

    private void SeguentPos()
    {
        if (act >= _rotacions.Count-1) act = 0;
        else act++;
        _objecteQueRota.transform.rotation = Quaternion.Euler(_rotacions[act]);
    }
}
