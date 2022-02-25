using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShot : MonoBehaviour
{
    [SerializeField] float _speed = 15f;
    public void Launch(Vector3 direction){
        direction.Normalize();
                // Debug.Log(transform.rotation.eulerAngles.x);   // x bằng 0 
        transform.up = direction;
                // Debug.Log(transform.rotation.eulerAngles.x);  // x bằng 90 
        GetComponent<Rigidbody>().velocity = direction * _speed;
    }

    private void OnCollisionEnter(Collision collision){
        Destroy(gameObject);
    }

    void Start(){
        Destroy(gameObject, 5f);
    }
}
