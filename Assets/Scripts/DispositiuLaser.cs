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
    private List<float> _aimWeights = new List<float>() { 0.5f, 1f};
    private float _timeToTurn=0.2f;
    private bool _turning=false;


    // Start is called before the first frame update
    void Start()
    {
        foreach(MultiAimConstraint constraint in _aimConstraints)
        {
            constraint.weight = 0;
        }
        Dispara(_target.position);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Dispara(Vector3 target)
    {
        StartCoroutine(turnToTarget(target));


    }

    private IEnumerator turnToTarget(Vector3 target)
    {
        _target.position = target;
        float time_elapsed = 0;
        while (time_elapsed < _timeToTurn)
        {
            time_elapsed += Time.deltaTime;
            float coef = Mathf.Clamp(time_elapsed / _timeToTurn,0,1);
            for(int i=0; i<_aimConstraints.Count; i++)
            {
                _aimConstraints[i].weight = Mathf.Lerp(0, _aimWeights[i], coef);
            }
            yield return null;
        }
    }

}
