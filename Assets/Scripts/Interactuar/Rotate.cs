using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : Interactuable
{
    [SerializeField]
    private bool _xAxis = false;
    [SerializeField]
    private bool _yAxis = false;
    [SerializeField]
    private bool _zAxis = false;
    [SerializeField]
    private float _amout = 90;
    public Transform _objecteQueRota;

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
        PlaySound();
        Vector3 rotation=new Vector3(_xAxis ? 1 : 0, _yAxis ? 1 : 0, _zAxis ? 1 : 0);
        _objecteQueRota.Rotate(rotation,_amout);
    }
}
