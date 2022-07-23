using UnityEngine;
using System;

public class PlayerControlsManager : MonoBehaviour
{
    [SerializeField] private GameObject _bossBattleManager;
    [SerializeField] private GameObject _playerObj;
    [SerializeField] private GameObject _bossArenaObj;
    private BossCamera _bossCameraScript;
    private PlayerHealthSystem _playerhealthScript;
    private BossPhase1 _bossPhase1;
    public static event Action _disableControls;
    public static event Action _enableControls;

    private void Start()
    {
        _bossPhase1 = _bossBattleManager.GetComponent<BossPhase1>();
        _playerhealthScript = _playerObj.GetComponent<PlayerHealthSystem>();
        _bossCameraScript = _bossArenaObj.GetComponent<BossCamera>();
    }

    private void Update()
    {
        if (!_playerhealthScript._isDead)
        {
            if (_bossPhase1._isAnswering && _bossCameraScript._isAtBossArena)
            {
                _disableControls?.Invoke();
            }
            if (!_bossPhase1._isAnswering)
            {
                _enableControls?.Invoke();
            }
        }
    }
}
