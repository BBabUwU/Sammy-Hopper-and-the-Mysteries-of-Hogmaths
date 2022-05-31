using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
  public Text _healthText;
  public Image _healthBar;
  float _health;
  float _maxHealth = 100f;
  float _lerpSpeed;

  private void Start()
  {
    _health = _maxHealth;
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
  }

  public void Heal(float _healingPoints)
  {
    if (_health < _maxHealth)
      _health += _healingPoints;
    _healthText.text = "Health: " + _health + "%";
  }
}
