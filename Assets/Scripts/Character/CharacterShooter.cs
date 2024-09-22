using System.Collections;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private CharacterShootingConfig _characterShootingConfig;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _projectileSpawnPoint;

    private bool _canShoot = true;
    private bool _isAttacking = false;  
    [SerializeField] private float _fireSpeed;
    [SerializeField] private float _damage;
    private Animator _animator;
    private float rotationSpeed = 10f;  

    private void Start()
    {
        _fireSpeed = _characterShootingConfig.fireSpeed;
        _damage = _characterShootingConfig.damage;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        GameObject nearestEnemy = FindNearestEnemy();
        RotateTowardsEnemy(nearestEnemy.transform.position);
        if (!_isAttacking)  
        {
            
            

            if (nearestEnemy != null)
            {
              

               
                if (_canShoot)
                {
                    StartCoroutine(Shoot(nearestEnemy));
                }
            }
        }
    }

  
    private void RotateTowardsEnemy(Vector3 enemyPosition)
    {
        Vector3 directionToEnemy = (enemyPosition - transform.position).normalized;
        directionToEnemy.y = 0; 

        Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private GameObject FindNearestEnemy()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None); 
        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Enemy enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(currentPosition, enemy.transform.position);
            if (distanceToEnemy < minDistance && enemy.enabled)
            {
                nearestEnemy = enemy.gameObject;
                minDistance = distanceToEnemy;
            }
        }
        return nearestEnemy;
    }

  
    private IEnumerator Shoot(GameObject targetEnemy)
    {
        _canShoot = false;
        _isAttacking = true; 

      
        _animator.SetTrigger("AttackTrigger");

      
        yield return new WaitForSeconds(0.2f); 

        GameObject projectile = Instantiate(_projectilePrefab, _projectileSpawnPoint.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.Launch(targetEnemy, _damage);
        }

        yield return new WaitForSeconds(1f / _fireSpeed);

        _canShoot = true;
        _isAttacking = false; 
    }
}
