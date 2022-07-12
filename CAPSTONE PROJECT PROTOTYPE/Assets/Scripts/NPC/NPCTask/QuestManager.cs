using UnityEngine;

public class QuestManager : MonoBehaviour
{
    int _enemyCounter;
    int _pages;
    private void Update()
    {
        Quest1Complete();
        Quest2Complete();
    }
    public void Quest1EnemyCounter(int x)
    {
        _enemyCounter = _enemyCounter + x;
    }

    public bool Quest1Complete()
    {
        if (_enemyCounter == 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Quest2PageCounter(int x)
    {
        _pages = _pages + x;
    }

    public bool Quest2Complete()
    {
        if (_pages == 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
