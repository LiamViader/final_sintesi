using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Observer : MonoBehaviour
{
    public class MyEvent: UnityEvent<float>{}

    public MyEvent notify;
    public void OnNotify(float value){
        notify.Invoke(value);
    }
}
