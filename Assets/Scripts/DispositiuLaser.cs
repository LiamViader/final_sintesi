using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations.Rigging;
using UnityEngine;

public class DispositiuLaser : MonoBehaviour
{
    [SerializeField]
    private Transform _invisibleTarget;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private List<MultiAimConstraint> _aimConstraints;
    private List<float> _aimWeights = new List<float>() {0.4f,0.6f,0.8f, 1f};
    [SerializeField]
    private ChainIKConstraint _pointingConstraint;
    private float _pointingWeight = 1f;
    private float _timeToTurn=0.4f;
    private float _timeToStop = 0.2f;
    private float _timeShooting = 5.5f;
    private bool _shooting=false;
    private bool _turning = false;
    private Coroutine _onGoingCoroutine=null;
    private Laser _laser;



    // Start is called before the first frame update
    void Start()
    {
        foreach(MultiAimConstraint constraint in _aimConstraints)
        {
            constraint.weight = 0;
        }
        _pointingConstraint.weight = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (_laser != null && _target!=null)
        {
            _invisibleTarget.position = _target.position;
            Vector3 dir = _target.position - transform.position;
            Vector3 norm = Vector3.Normalize(dir);
            _laser.UpdateDirection(norm);
        }
        else if(_shooting && _laser==null && !_turning)
        {
            StopShooting();
        }
    }

    public void Shoot(Transform target) //EL target ha de ser l'aparença
    {
        if (!_turning && !_shooting && _target==null)
        {
            _turning = true;
            _onGoingCoroutine = StartCoroutine(TurnAndPointToTarget(target));
        }


    }

    public void StopShooting()
    {
        _onGoingCoroutine = StartCoroutine(StopPointingAndFacing());
    }

    private IEnumerator StopPointingAndFacing()
    {
        _turning = true;
        float time_elapsed = 0;
        while (time_elapsed < _timeToStop)
        {
            time_elapsed += Time.deltaTime;
            float coef = 1-Mathf.Clamp(time_elapsed / _timeToStop, 0, 1);
            for (int i = 0; i < _aimConstraints.Count; i++)
            {
                _aimConstraints[i].weight = Mathf.Lerp(0, _aimWeights[i], coef);
            }
            _pointingConstraint.weight = Mathf.Lerp(0, _pointingWeight, coef);
            yield return null;
        }
        _turning = false;
        _shooting = false;
        _target = null;
    }

    private IEnumerator TurnAndPointToTarget(Transform target)
    {
        _target = target;
        _invisibleTarget.position = target.position;
        float time_elapsed = 0;
        while (time_elapsed < _timeToTurn)
        {
            time_elapsed += Time.deltaTime;
            float coef = Mathf.Clamp(time_elapsed / _timeToTurn,0,1);
            for(int i=0; i<_aimConstraints.Count; i++)
            {
                _aimConstraints[i].weight = Mathf.Lerp(0, _aimWeights[i], coef);
            }
            _pointingConstraint.weight = Mathf.Lerp(0, _pointingWeight, coef);
            yield return null;
        }
        _turning = false;
        GameObject instance = Instantiate(_laserPrefab);
        _laser = instance.GetComponent<Laser>();
        Vector3 dir = target.position - transform.position;
        Vector3 norm = Vector3.Normalize(dir);
        _laser.Init(transform, norm, _timeShooting);
        _shooting = true;
    }


}
