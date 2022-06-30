using UnityEngine;

public class Quest1 : MonoBehaviour
{

    GameObject _questMObj;
    QuestManager _questMScript;
    private void Start()
    {
        _questMObj = GameObject.FindGameObjectWithTag("QuestManager");
        _questMScript = _questMObj.GetComponent<QuestManager>();
    }
    private void OnDestroy()
    {
        _questMScript.Quest1EnemyCounter(1);
    }
}
