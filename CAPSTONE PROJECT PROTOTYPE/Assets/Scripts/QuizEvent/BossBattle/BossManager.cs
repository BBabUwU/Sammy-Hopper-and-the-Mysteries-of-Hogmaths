using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] private GameObject _bossUI;
    [SerializeField] private GameObject _bossArea;
    private BossCamera _bossCam;
    private BossPhase1 _phase1;

    private void Start()
    {
        _bossCam = _bossArea.GetComponent<BossCamera>();
        _phase1 = gameObject.GetComponent<BossPhase1>();
    }

    private void Update()
    {
        StartBoss();
    }

    private void StartBoss()
    {
        if (_bossCam._isAtBossArena)
        {
            _phase1.enabled = true;
            _bossUI.SetActive(true);
        }
    }
}
