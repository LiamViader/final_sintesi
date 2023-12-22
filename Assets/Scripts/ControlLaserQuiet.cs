using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLaserQuiet : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;
    private Laser _laser;
    void Start()
    {
        GameObject instance = Instantiate(_laserPrefab);
        _laser = instance.GetComponent<Laser>();
        _laser.Init(transform.position, transform.forward, -1, transform.localScale.magnitude*0.1f, false);
        _laser.Subscribe(OnLaserDestroy);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _laser.UpdateDirection(transform.forward, transform.position, transform.localScale.magnitude * 0.3f);
    }

    private void OnLaserDestroy(Laser laser)
    {
        laser.Unsubscribe(OnLaserDestroy);
    }

}
