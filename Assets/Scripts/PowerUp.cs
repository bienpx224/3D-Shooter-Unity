using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float _duration = 5f;
    [SerializeField] float _delayMultiplier = 0.5f;
    [SerializeField] float _countDownTime = 5f;
    [SerializeField] Transform[] _spawnPoints;

    [SerializeField] bool _spreadShot = false;
    public bool SpreadShot {
        get {return _spreadShot;}
        set { _spreadShot = value; }
    }
    private PlayerWeapon playerWeapon;

    public float DelayMultiplier => _delayMultiplier; 
    void OnTriggerEnter(Collider other){
        playerWeapon = other.GetComponent<PlayerWeapon>();
        if(playerWeapon){
            playerWeapon.AddPowerup(this);
            StartCoroutine(DisableAfterDelay());
            GetComponent<Collider>().enabled = false;
            GetComponent<Renderer>().enabled = false;
        }
    }
    IEnumerator DisableAfterDelay(){
        yield return new WaitForSeconds(_duration);
        playerWeapon.RemovePowerup(this);
        yield return new WaitForSeconds(_countDownTime);
        int randomIndex = Random.Range(0, _spawnPoints.Length);

        transform.position = _spawnPoints[randomIndex].position;
        GetComponent<Collider>().enabled = true;
        GetComponent<Renderer>().enabled = true;
    }
}
