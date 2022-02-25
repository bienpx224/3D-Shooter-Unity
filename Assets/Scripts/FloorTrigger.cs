using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloorTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent _onEntered;
    [SerializeField] UnityEvent _onExited;
    [SerializeField] float _exitDelay = 3;

    int _entered;

    void OnTriggerEnter(Collider other){
        _onEntered.Invoke();
        _entered++;
    }

    void OnTriggerExit(Collider other){
        _entered--;
        if(_entered <= 0){
            StartCoroutine(ExitAfterDelay());
        }
    }

    IEnumerator ExitAfterDelay(){
        yield return new WaitForSeconds(_exitDelay);
        _onExited.Invoke();
    }

}
