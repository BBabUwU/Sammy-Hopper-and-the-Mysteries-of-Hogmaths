using UnityEngine;

public class InteractableNPC : MonoBehaviour
{

    private bool _isInteractable;
    public GameObject _dialouge;
    private QuestManager _QuestManagerScript;
    private void Start()
    {
        _QuestManagerScript = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _isInteractable)
        {
            if (gameObject.tag == "Quest1NPC")
            {
                if (_QuestManagerScript.Quest1Complete())
                {
                    Debug.Log("Dialouge Changed");
                    _dialouge = GameObject.FindWithTag("Quest1Dialogue").transform.GetChild(1).gameObject;
                    _dialouge.SetActive(true);
                }
            }

            if (gameObject.tag == "Quest2NPC")
            {
                if (_QuestManagerScript.Quest2Complete())
                {
                    Debug.Log("Dialouge Changed");
                    _dialouge = GameObject.FindWithTag("Quest2Dialogue").transform.GetChild(1).gameObject;
                    _dialouge.SetActive(true);
                }
            }

            _dialouge.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _isInteractable = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _isInteractable = false;
        _dialouge.SetActive(false);
    }
}
