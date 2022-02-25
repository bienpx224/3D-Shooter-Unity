using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] float _delayFireTime = 0.25f;
    [SerializeField] BulletShot _bulletShot;

    [SerializeField] LayerMask _aimLayerMask;
    [SerializeField] Transform _firePoint;
    [SerializeField] bool _spread;
    public bool SpreadShot => _spread;
    float _nextFireTime;

    List<PowerUp> _powerups = new List<PowerUp>();


    // Update is called once per frame
    void Update()
    {
        AimTowardMouse();
        if (ReadyToFire())
        {
            Fire();
        }
    }

    void AimTowardMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _aimLayerMask))
        {
            var destination = hit.point;
            destination.y = transform.position.y;

            Vector3 direction = destination - transform.position;
            direction.Normalize();
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

        }
    }

    bool ReadyToFire() => Time.time >= _nextFireTime;


    void Fire()
    {
        float delay = _delayFireTime;
        foreach (var powerUp in _powerups)
        {
            delay *= powerUp.DelayMultiplier;
        }
        _nextFireTime = Time.time + delay;
        BulletShot shot = Instantiate(_bulletShot,
            _firePoint.position,
            transform.rotation
        );

        shot.Launch(transform.forward);

        if (_powerups.Any(t => t.SpreadShot))
        {
            Debug.Log("SpreadShot start ");
            shot = Instantiate(_bulletShot,
                _firePoint.position,
                Quaternion.Euler(transform.forward + transform.right));
            shot.Launch(transform.forward + transform.right);

            shot = Instantiate(_bulletShot,
                _firePoint.position,
                Quaternion.Euler(transform.forward - transform.right));
            shot.Launch(transform.forward - transform.right);
        }
    }

    public void RemovePowerup(PowerUp powerUp) => _powerups.Remove(powerUp);

    public void AddPowerup(PowerUp powerUp) => _powerups.Add(powerUp);
}
