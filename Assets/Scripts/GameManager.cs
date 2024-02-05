using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; private set; } // Singleton pattern

  private bool isGameOver = false;

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public void GameOver()
  {
    if (!isGameOver)
    {
      isGameOver = true;
      Debug.Log("Game Over!");
    }
  }

  public bool IsGameOver()
  {
    return isGameOver;
  }
}