using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    //Quest 1 variables
    int _enemyCounter;
    private void Update()
    {
        Quest1Complete();
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
}
