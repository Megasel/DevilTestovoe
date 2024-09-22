using UnityEngine;
using UnityEngine.UI;

public class CharacterHealthSystem : MonoBehaviour
{
    [SerializeField] private CharacterHealthConfig _characterHealthConfig;
    public float Health { get { return _health; } set { _health = Mathf.Clamp(value, 0, _maxHealth); } }
    private float _maxHealth;
    [SerializeField] private float _health;
    [SerializeField] private Slider _healthSlider;
    private Animator _animator;
    [Header("UI")]
    [SerializeField] private GameObject _gameOverPanel;
    
    private void Start()
    {
        _health = _characterHealthConfig.maxHealth;
        _maxHealth = _health;
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        UpdateUi();
        if(Health <= 0)
        {
            Death();
            _animator.SetTrigger("DeathTrigger");
            _animator.SetLayerWeight(1, 0);
        }
    }
    private void Death()
    {
        _gameOverPanel.SetActive(true);
    }
    private void UpdateUi()
    {
        _healthSlider.value = _health/_maxHealth ;
    }
}
