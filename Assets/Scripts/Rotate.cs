using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MeshInteractuable
{
    public bool _xAxis = false;
    public bool _yAxis = false;
    public bool _zAxis = false;
    public float _amout = 90;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        Vector3 rotation=new Vector3(_xAxis ? 90 : _amout, _yAxis ? _amout : 0, _zAxis ? _amout : 0);
        transform.Rotate(rotation);
    }
}
