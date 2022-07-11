using UnityEngine;

public class BlockedPath : MonoBehaviour
{
    public GameObject _quizEvent;
    private QuizManager _boolScript;
    private bool _isInteractable;
    private bool _ableToMove = true;

    GameObject _player;
    PlayerMovement _playerMovement;
    Weapon _playerWeapon;

    private void Start()
    {
        _boolScript = transform.parent.GetComponent<QuizManager>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerMovement = _player.GetComponent<PlayerMovement>();
        _playerWeapon = _player.GetComponent<Weapon>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _isInteractable)
        {
            Debug.Log("Clicked F");
            _ableToMove = false;
            _quizEvent.SetActive(true);
        }

        if (_ableToMove)
        {
            _playerMovement.enabled = true;
            _playerWeapon.enabled = true;
        }

        if (!_ableToMove)
        {
            _playerMovement.enabled = false;
            _playerWeapon.enabled = false;
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

    public void SetPlayerControl(bool _setPlayerControl)
    {
        _ableToMove = _setPlayerControl;
    }

    private void Passed()
    {
        if (_boolScript._isPassed)
        {
            _playerMovement.enabled = true;
            _playerWeapon.enabled = true;
            Destroy(this.transform.parent.gameObject);
        }
    }

}
