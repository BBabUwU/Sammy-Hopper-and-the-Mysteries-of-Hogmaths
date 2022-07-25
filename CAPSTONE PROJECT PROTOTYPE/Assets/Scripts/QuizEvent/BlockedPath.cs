using UnityEngine;

public class BlockedPath : MonoBehaviour
{
    public GameObject _quizEvent;
    private QuizManager _boolScript;
    private bool _isInteractable;
    [SerializeField] private PlayerManager _playerManager;

    GameObject _player;


    private void Start()
    {
        _boolScript = transform.parent.GetComponent<QuizManager>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _isInteractable)
        {
            Debug.Log("Clicked F");
            _quizEvent.SetActive(true);
            _playerManager.EnableMovement(false);
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

    public void ExitButton()
    {
        _quizEvent.SetActive(false);
        _playerManager.EnableMovement(true);
    }

    private void Passed()
    {
        if (_boolScript._isPassed)
        {
            _playerManager.EnableMovement(true);
            Destroy(this.transform.parent.gameObject);
        }
    }

}
