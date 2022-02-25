using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.SceneManagement;

public class Zombie : MonoBehaviour
{

    public static event Action<Zombie> Died;

    [SerializeField] float _attackRange = 1f;
    [SerializeField] int _health = 3;
    private int _currentHealth;
    NavMeshAgent _navMeshAgent;
    private Animator _animator;

    bool Alive => _currentHealth > 0;
    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _health;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _navMeshAgent.enabled = true;
    }
    public void StartWalking(){
        _currentHealth = _health;
        _navMeshAgent.enabled = true;
    }
    void OnCollisionEnter(Collision collision)
    {
        var bulletShot = collision.collider.GetComponent<BulletShot>();
        if (bulletShot != null)
        {
            Shooten();
        }
    }
    void Shooten()
    {
        _currentHealth--;
        if (_currentHealth <= 0)
        {
            Die();
        }
        else
        {
            ShootenAnim();
        }
    }
    void ShootenAnim()
    {
        _navMeshAgent.enabled = false;
        StartCoroutine(MoveAfterShooten(0.3f));
        _animator.SetTrigger("Shooten");
    }
    void Die()
    {
        GetComponent<Collider>().enabled = false;
        _navMeshAgent.enabled = false;
        _animator.SetTrigger("Dead");
        Died?.Invoke(this);
        Destroy(gameObject, 3.0f);  // OR 
        // StartCoroutine(RemoveWhenDead(2.0f));
        
    }
    IEnumerator MoveAfterShooten(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _navMeshAgent.enabled = true;
    }
    IEnumerator RemoveWhenDead(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if(!Alive) return;
        var player = FindObjectOfType<PlayerMovement>();
        if (_navMeshAgent.enabled)
            _navMeshAgent.SetDestination(player.transform.position);

        if (Vector3.Distance(transform.position, player.transform.position) < _attackRange)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _navMeshAgent.enabled = false;
        _animator.SetTrigger("Attack");
    }
    // Anim callback
    void AttackComplete()
    {
        if (Alive)
            _navMeshAgent.enabled = true;
    }
    void AttackHit()
    {
        // Debug.Log("Attack HIT");
        var player = FindObjectOfType<PlayerMovement>();
        if(Vector3.Distance(transform.position, player.transform.position) <= _attackRange * 2){
            SceneManager.LoadScene(0);
        }

    }
    void ShootenComplete()
    {
        // _navMeshAgent.enabled = false;
    }
    void ShootenStart()
    {
        // _navMeshAgent.enabled = false;
    }

}
