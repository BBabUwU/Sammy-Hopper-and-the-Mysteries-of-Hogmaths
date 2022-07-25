using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    private bool _canMove = true;
    private bool _isDead = false;
    //Delegates
    public static event Action _disableControls;
    public static event Action _enableControls;
    public static event Action _onPlayerDeath;

    private void Update()
    {
        if (!_isDead)
        {
            if (_canMove)
            {
                _enableControls?.Invoke();
            }
            else
            {
                _disableControls?.Invoke();
            }
        }
        else if (_isDead)
        {
            _onPlayerDeath?.Invoke();
            _disableControls?.Invoke();
        }
    }

    public bool EnableMovement(bool x)
    {
        _canMove = x;
        return _canMove;
    }

    public bool SetPlayerDeath(bool x)
    {
        _isDead = x;
        return _isDead;
    }
}
