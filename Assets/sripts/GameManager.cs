using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public UIHandler uiHandler;
    public float restartDelay = 3f;

    EnemyController[] enemies;
    bool gameOver;

    void Start()
    {
        enemies = FindObjectsByType<EnemyController>(
            FindObjectsSortMode.None);
        uiHandler.SetEnemyTotal(enemies.Length);
    }

    void Update()
    {
        if (gameOver)
        {
            return;
        }

        if (player.health <= 0)
        {
            gameOver = true;
            uiHandler.DisplayLoseScreen();
            Invoke(nameof(ReloadScene), restartDelay);
        }
        else if (AllEnemiesFixed())
        {
            gameOver = true;
            uiHandler.DisplayWinScreen();
            Invoke(nameof(ReloadScene), restartDelay);
        }
    }

    bool AllEnemiesFixed()
    {
        foreach (EnemyController enemy in enemies)
        {
            if (enemy.isBroken)
            {
                return false;
            }
        }
        return true;
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
