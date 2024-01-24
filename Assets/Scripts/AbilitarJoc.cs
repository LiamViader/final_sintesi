using UnityEngine;
using System.Collections;
using UnityEngine.Events;
    public class SimpleScript : MonoBehaviour
    {

        public UnityEvent abilitar;
        public UnityEvent desabilitar;
        void Start(){
        }
        public void Abilitar()
        {
          abilitar.Invoke();
        }

        public void Desabilitar()
        {
            desabilitar.Invoke();      
        }
    }
