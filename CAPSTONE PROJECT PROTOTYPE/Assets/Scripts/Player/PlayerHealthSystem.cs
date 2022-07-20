using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    public static event Action _OnPlayerDeath;
    public Image _healthBar;
    float _health;
    float _maxHealth = 100f;
    float _lerpSpeed;
    public GameObject _playerObject;
    Animator _playerAnimator;
    public bool _isDead = false;
    private Rigidbody2D _rb;

    private void Start()
    {
        _health = 50f;
        _playerAnimator = _playerObject.GetComponent<Animator>();
        _rb = _playerObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_health > _maxHealth) _health = _maxHealth;
        _lerpSpeed = 3f * Time.deltaTime;
        HealthBarFiller();
        ColorChanger();
    }

    void HealthBarFiller()
    {
        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, _health / _maxHealth, _lerpSpeed);
    }

    void ColorChanger()
    {
        Color _healthColor = Color.Lerp(Color.red, Color.green, (_health / _maxHealth));
        _healthBar.color = _healthColor;
    }

    public void Damage(float _damagePoints)
    {
        if (_health > 0)
            _health -= _damagePoints;

        if (_health <= 0)
        {
            if (!_isDead)
            {
                _playerAnimator.SetTrigger("IsDead");
                _isDead = true;
            }
            _OnPlayerDeath?.Invoke();
        }

    }

    public void Heal(float _healingPoints)
    {
        if (_health < _maxHealth)
            _health += _healingPoints;
    }
}
