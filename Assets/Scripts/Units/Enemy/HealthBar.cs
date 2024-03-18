using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _current;
    [SerializeField] private EnemyHealth _enemyHealth;
    [SerializeField] private Canvas _canvas;

    private float _currentFillAmount;
    private Transform _camera;

    public void Start()
    {
        _camera = Camera.main.transform;
        _currentFillAmount = _enemyHealth.CurrentHealth;
    }

    void OnEnable() => 
        _enemyHealth.OnTakeDamage += ChangeHealth;

    void OnDisable() => 
        _enemyHealth.OnTakeDamage -= ChangeHealth;


    protected void ChangeHealth(float currentHealth) => 
        _current.fillAmount = currentHealth / _currentFillAmount;

    private void LateUpdate() => 
        _canvas.transform.LookAt(_camera);
}