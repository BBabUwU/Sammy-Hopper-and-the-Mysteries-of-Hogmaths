using UnityEngine;

public class Quest2 : MonoBehaviour
{
    GameObject _questMObj;
    QuestManager _questMScript;
    private void Start()
    {
        _questMObj = GameObject.FindGameObjectWithTag("QuestManager");
        _questMScript = _questMObj.GetComponent<QuestManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        _questMScript.Quest2PageCounter(1);
        Destroy(this.transform.gameObject);
    }
}
