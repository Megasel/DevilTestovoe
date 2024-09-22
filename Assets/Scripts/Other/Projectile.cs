using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject _target;   
    private float _damage;         
    private float _speed = 10f;    

   
    public void Launch(GameObject targetEnemy, float damageAmount)
    {
        _target = targetEnemy;
        _damage = damageAmount;

       
        if (_target != null)
        {
            Vector3 directionToTarget = (_target.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(directionToTarget);
        }
    }

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject); 
            return;
        }

       
        Vector3 direction = (_target.transform.position - transform.position).normalized;
        transform.position += direction * _speed * Time.deltaTime;

       
        transform.rotation = Quaternion.LookRotation(direction);

     
        if (Vector3.Distance(transform.position, _target.transform.position) < 0.1f)
        {
            HitTarget();
        }
    }

    private void HitTarget()
    {
      
        Enemy enemy = _target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }
}
