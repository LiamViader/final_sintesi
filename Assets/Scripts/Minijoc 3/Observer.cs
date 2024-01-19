using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Observer : MonoBehaviour
{
    public UnityEvent notify;
    public void OnNotify(float value, float tipe){
        notify.Invoke();
    }
}
