using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Laser : MonoBehaviour
{
    public delegate void HandlerDestruccion();


    private const float MAXDIST = 100f;
    private bool _initialized = false;
    private Vector3 _source;
    private Vector3 _direction;
    private float _timeLeft;
    private bool _timeLimitless = false;
    private float width_coef = 0.5f;
    private float _width = 1f;
    private bool _finished = false;
    private bool _destruirControlatPerDisparable = true;

    private float _animCoef = 0;
    private float _animTime = 0.3f;

    public event Action<Laser> _OnLaserRemoved;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (_timeLeft <= 0 + _animTime && !_finished)
        {
            Finish();
           
        }
        if (_initialized && _timeLeft>0)
        {
            if(!_timeLimitless)_timeLeft -= Time.fixedDeltaTime;

            RaycastHit hit;
            float length=MAXDIST;
            if (Physics.Raycast(_source, _direction, out hit, MAXDIST,CameresHabitacio.getIgnoreMask()))
            {
                Vector3 posHit = hit.point;
                length = (posHit - _source).magnitude;
                if (!_finished)
                {
                    Disparable enc;
                    if (hit.collider.TryGetComponent<Disparable>(out enc))
                    {
                        if (enc.TocatPelLaser(this,posHit) && _destruirControlatPerDisparable) Finish();
                    }
                }

            }
            Vector3 scale = new Vector3(_width, _width, length / 2) * _animCoef;
            Vector3 pos = _source + (_direction*length / 2)*_animCoef;

            transform.localScale = scale;
            transform.position = pos;
            transform.LookAt(_source + _direction);
        }
    }

    public void Init(Vector3 source, Vector3 direction, float duration, float width) // destruirContolatPerDisparable=true per defecte
    {
        _width = width*width_coef;
        if (duration < 0)
        {
            _timeLimitless = true;
            _timeLeft = 10000f;
        }
        else _timeLeft = duration;
        _source = source;
        _direction = direction;
        _initialized = true;
        StartCoroutine(StartAnimation());
    }

    public void Init(Vector3 source, Vector3 direction, float duration, float width, bool destruirControlatPerDisparable)
    {
        _width = width*width_coef;
        _destruirControlatPerDisparable = destruirControlatPerDisparable;
        if (duration < 0)
        {
            _timeLimitless = true;
            _timeLeft = 10000f;
        }
        else _timeLeft = duration;
        _source = source;
        _direction = direction;
        _initialized = true;
        StartCoroutine(StartAnimation());
    }


    public void UpdateDirection(Vector3 direction, Vector3 source, float width)
    {
        _width = width*width_coef;
        _direction = direction;
        _source = source;
    }

    public void Finish()
    {
        _finished = true;
        StartCoroutine(EndAnimation());
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
        FinishImmediately();
    }


    public Vector3 Direction()
    {
        return _direction;
    }

    public void FinishImmediately()
    {
        _OnLaserRemoved?.Invoke(this);
        Destroy(gameObject);
    }

    public void Unsubscribe(Action<Laser> WantsToUnsubscribe)
    {
        _OnLaserRemoved -= WantsToUnsubscribe;
    }

    public void Subscribe(Action<Laser> WantsToUnsubscribe)
    {
        _OnLaserRemoved += WantsToUnsubscribe;
    }

    public bool DestruitControlatPerDisparable()
    {
        return _destruirControlatPerDisparable;
    }

    public float Width()
    {
        return _width/width_coef;
    }
}
