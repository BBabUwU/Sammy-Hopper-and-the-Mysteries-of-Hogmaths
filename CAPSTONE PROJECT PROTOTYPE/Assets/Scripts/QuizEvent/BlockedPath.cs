using UnityEngine;

public class BlockedPath : MonoBehaviour
{
    public GameObject _quizEvent;
    public QuizManager _boolScript;
    private bool _isInteractable;

    private void Start()
    {
        _boolScript = GameObject.Find("QuizManager").GetComponent<QuizManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _isInteractable)
        {
            Debug.Log("Clicked F");
            _quizEvent.SetActive(true);
        }

        Passed();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _isInteractable = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _isInteractable = false;
        _quizEvent.SetActive(false);
    }

    private void Passed()
    {
        if (_boolScript._isPassed)
        {
            Destroy(this.transform.parent.gameObject);
        }
    }

}
