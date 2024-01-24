using UnityEngine;
using System.Collections;
using UnityEngine.Events;
    public class SimpleScript : MonoBehaviour
    {

        [SerializeField] public MinijocInteractuable funciona;
        public UnityEvent abilitar;
        public UnityEvent desabilitar;
        void Start(){
        }
        void Update()
        {
            if (funciona.getInteractuar()){
                Debug.Log("Abilitar");
                abilitar.Invoke();
            }
            else{
                desabilitar.Invoke();
            }
        }

    }
