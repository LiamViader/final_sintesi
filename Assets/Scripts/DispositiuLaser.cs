using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations.Rigging;
using UnityEngine;

public class DispositiuLaser : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private List<MultiAimConstraint> _aimConstraints;
    private List<float> _aimWeights = new List<float>() {0.4f,0.6f,0.8f, 1f};
    [SerializeField]
    private ChainIKConstraint _pointingConstraint;
    private float _pointingWeight = 1f;
    private float _timeToTurn=0.4f;
    private bool _turning=false;

    float a = 0;


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
        a += 0.02f;
        if(a>3) Shoot(_target); 
    }

    public void Shoot(Transform target)
    {
        StartCoroutine(turnAndPointToTarget(target));


    }

    private IEnumerator turnAndPointToTarget(Transform target)
    {
        _target= target;
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
    }

}
