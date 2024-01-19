using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour
{
   private List<Observer> _observers = new List<Observer>();
   

    public void AddObserver(Observer observer){
        if (! _observers.Contains(observer)){
            _observers.Add(observer);
        }
        
    }

    public void RemoveObserver(Observer observer){
         if (_observers.Contains(observer)){
            _observers.Remove(observer);
        }
         
    }
    protected void NotifyObservers(float value){
        _observers.ForEach((_observer)=> {
            _observer.OnNotify(value);
        });

    }
}
