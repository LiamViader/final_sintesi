using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private const float MAXDIST = 100f;
    private bool _initialized = false;
    private Transform _source;
    private Vector3 _direction;
    private float _timeLeft;
    private float _width = 0.05f;
    private bool _finished = false;

    private float _animCoef = 0;
    private float _animTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (_timeLeft <= 0 + _animTime && !_finished)
        {
            _finished = true;
            StartCoroutine(EndAnimation());
        }
        if (_initialized && _timeLeft>0)
        {
            _timeLeft -= Time.fixedDeltaTime;

            RaycastHit hit;
            float length=MAXDIST;
            if (Physics.Raycast(_source.position, _direction, out hit, MAXDIST))
            {
                Vector3 posHit = hit.transform.position;
                length = (posHit - _source.position).magnitude;
            }
            Vector3 scale = new Vector3(_width, _width, length / 2) * _animCoef;
            Vector3 pos = _source.position + (_direction*length / 2)*_animCoef;

            transform.localScale = scale;
            transform.position = pos;
            transform.LookAt(_source.position + _direction);
        }
    }

    public void Init(Transform source, Vector3 direction, float duration)
    {
        _timeLeft = duration;
        _source = source;
        _direction = direction;
        _initialized = true;
        StartCoroutine(StartAnimation());
    }

    public void UpdateDirection(Vector3 direction)
    {
        _direction = direction;
    }

    private IEnumerator StartAnimation()
    {
        float time_elapsed = 0;
        while (time_elapsed < _animTime)
        {
            time_elapsed += Time.deltaTime;
            _animCoef = time_elapsed / _animTime;
            yield return null;
        }
        _animCoef = 1;
    }
    private IEnumerator EndAnimation()
    {
        float time_elapsed = 0;
        while (time_elapsed < _animTime)
        {
            time_elapsed += Time.deltaTime;
            _animCoef = 1-(time_elapsed / _animTime);
            yield return null;
        }
        Destroy(gameObject);
    }


}
