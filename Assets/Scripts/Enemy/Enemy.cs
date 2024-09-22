using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public float moveSpeed = 5f;
    public float damage = 10f;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _attackInterval = 2f;
    [SerializeField] private float _attackRange = 1.5f;

    private Transform _healthBarTransform; 
    [SerializeField] private Slider _healthSlider;         

    private Transform _player;
    private bool _canAttack = true;
    private CharacterHealthSystem _characterHealthSystem;
    private Canvas _canvas;
    private Animator _animator;
    private Vector3 _currentDirection = Vector3.zero;

    private void Start()
    {
        _characterHealthSystem = FindAnyObjectByType<CharacterHealthSystem>();
        _player = _characterHealthSystem.transform;
        
        _animator = GetComponent<Animator>();
        health = _maxHealth;
        _healthSlider = GetComponentInChildren<Slider>();
        _canvas = GetComponentInChildren<Canvas>();
        _healthSlider.maxValue = _maxHealth;
        _healthSlider.value = health;

        StartCoroutine(MoveTowardsPlayer());
    }

    private void Update()
    {
        
            _canvas.transform.rotation = Camera.main.transform.rotation;
            //Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2, 0)); 
            //_healthBarTransform.position = screenPosition;
        
    }

    private void RotateCharacter(Vector3 moveDirection)
    {
        if (Vector3.Angle(transform.forward, moveDirection) > 0)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDirection, 10 * Time.deltaTime, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    private IEnumerator MoveTowardsPlayer()
    {
        while (health > 0)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

            if (distanceToPlayer > _attackRange)
            {
                _currentDirection = (_player.position - transform.position).normalized;
                _currentDirection.y = 0; 
                transform.position += _currentDirection * moveSpeed * Time.deltaTime;

                _animator.SetFloat("BlendEnemySpeed", 1, 0.1f, Time.deltaTime);
            }
            else
            {
                _animator.SetFloat("BlendEnemySpeed", 0, 0.1f, Time.deltaTime);

                if (_canAttack)
                {
                    StartCoroutine(AttackPlayer());
                }
            }

            RotateCharacter(_currentDirection);
            yield return null;
        }
    }

    private IEnumerator AttackPlayer()
    {
        _canAttack = false;

        _animator.SetTrigger("AttackTrigger");

        Debug.Log($"Enemy attacks! Deals {damage} damage.");
        _characterHealthSystem.TakeDamage(damage);

        yield return new WaitForSeconds(_attackInterval);

        _canAttack = true;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            health = 0;
            StartCoroutine(Die());
        }

        _healthSlider.value = health;
    }

    private IEnumerator Die()
    {
        enabled = false;
        _animator.SetTrigger("DeathTrigger");
        _animator.SetLayerWeight(1, 0);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
