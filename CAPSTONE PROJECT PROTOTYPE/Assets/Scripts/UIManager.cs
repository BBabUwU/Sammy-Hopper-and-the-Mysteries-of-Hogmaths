using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject _gameOverMenu;

    private void OnEnable()
    {
        PlayerManager._onPlayerDeath += EnableGameOverMenu;
    }

    private void OnDisable()
    {
        PlayerManager._onPlayerDeath -= EnableGameOverMenu;
    }

    public void EnableGameOverMenu()
    {
        _gameOverMenu.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }
}
