using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
  public event Action<int> OnHealthChanged;
  public static PlayerHealth Instance { get; private set; } // Static instance
  [SerializeField] private int health = 20;

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public void TakeDamage(int damage)
  {
    health -= damage;

    OnHealthChanged?.Invoke(health);

    if (health <= 0)
    {
      GameManager.Instance.GameOver();
    }
  }

  public int GetPlayerHealth()
  {
    return health;
  }
}
