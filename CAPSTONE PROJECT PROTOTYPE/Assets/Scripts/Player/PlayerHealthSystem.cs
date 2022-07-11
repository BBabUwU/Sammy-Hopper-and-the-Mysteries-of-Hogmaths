using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    public static event Action _OnPlayerDeath;
    public Text _healthText;
    public Image _healthBar;
    float _health;
    float _maxHealth = 100f;
    float _lerpSpeed;
    public GameObject _playerObject;
    Animator _playerAnimator;

    private void Start()
    {
        _health = 50f;
        _healthText.text = "Health: " + _health + "%";
        _playerAnimator = _playerObject.GetComponent<Animator>();
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
        _healthText.text = "Health: " + _health + "%";

        if (_health <= 0)
        {
            _playerObject.layer = LayerMask.NameToLayer("IgnorePlayer");
            _playerAnimator.SetTrigger("IsDead");
            _OnPlayerDeath?.Invoke();
        }

    }

    public void Heal(float _healingPoints)
    {
        if (_health < _maxHealth)
            _health += _healingPoints;
        _healthText.text = "Health: " + _health + "%";
    }
}
