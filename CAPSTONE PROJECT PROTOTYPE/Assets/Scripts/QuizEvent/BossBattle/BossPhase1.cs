using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BossPhase1 : MonoBehaviour
{
    //Boss UI
    [SerializeField] private GameObject _bossUI;
    private TMP_InputField _inputField;
    private TMP_Text _questionText;
    [NonReorderable] public List<QnA> _qna = new List<QnA>();
    private bool _isPassed = false;
    public bool _isAnswering = true;
    private int _currentQuestionIndex;
    public Animator _startBossFight;

    //Properties
    [SerializeField] private int _maximumNumberOfQuestions;
    private int _passingScore;
    private int _questionCounter = 0;
    private int _correctAnswerCounter = 0;
    [SerializeField] public int _damageBonusMultiplier = 0;
    private string _input;

    //Timer
    public float _timeLeft;
    public bool _TimerActive = false;
    private TMP_Text _timerTxt;
    private GameObject _scorePanel;
    private GameObject _questionPanel;


    void Start()
    {
        _questionText = _bossUI.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();

        _timerTxt = _bossUI.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();

        _inputField = _bossUI.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<TMP_InputField>();

        _questionPanel = _bossUI.transform.GetChild(0).gameObject;
        _scorePanel = _bossUI.transform.GetChild(1).gameObject;

        _timeLeft = _timeLeft * 60;
        _TimerActive = true;
        _passingScore = (Mathf.Abs(_maximumNumberOfQuestions / 2));

        RandomizeQuestion();

        _bossUI.SetActive(true);
    }

    private void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if (_TimerActive)
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
                UpdateTimer(_timeLeft);
            }
            else
            {
                Evaluation();
            }
        }
    }

    void UpdateTimer(float _currentTime)
    {
        _currentTime += 1;
        float _minutes = Mathf.FloorToInt(_currentTime / 60);
        float _seconds = Mathf.FloorToInt(_currentTime % 60);

        _timerTxt.text = string.Format("{0:00} : {1:00}", _minutes, _seconds);
    }

    private void RandomizeQuestion()
    {
        _currentQuestionIndex = Random.Range(0, _qna.Count);
        _questionText.text = _qna[_currentQuestionIndex]._question;
    }

    private void CorrectAnswer()
    {
        if (_questionCounter != _maximumNumberOfQuestions)
        {
            if (_input == _qna[_currentQuestionIndex]._correctAnswer)
            {
                _damageBonusMultiplier++;
                _correctAnswerCounter++;
            }

            _questionCounter++;
        }

        Debug.Log(_damageBonusMultiplier);

        if (_questionCounter == _maximumNumberOfQuestions)
        {
            Evaluation();
        }
        else
        {
            _qna.RemoveAt(_currentQuestionIndex);
            RandomizeQuestion();
        }
    }

    private void Evaluation()
    {
        _timeLeft = 0;
        _TimerActive = false;
        _questionPanel.SetActive(false);
        _scorePanel.SetActive(true);
        TMP_Text _scoreMessage = _scorePanel.transform.GetChild(1).GetComponent<TMP_Text>();
        TMP_Text _passMessage = _scorePanel.transform.GetChild(2).GetComponent<TMP_Text>();

        _scoreMessage.text = string.Format("{0:00} / {1:00}", _correctAnswerCounter, _maximumNumberOfQuestions);
        if (_correctAnswerCounter >= _passingScore)
        {
            _isPassed = true;
            _passMessage.SetText("YOU PASSED (OUT)");
        }
        else
        {
            _isPassed = false;
            _passMessage.SetText("YOU PASSN'T");
        }

        StartCoroutine(PassMessageDelay());
    }

    IEnumerator PassMessageDelay()
    {
        PlayerHealthSystem _playerhealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthSystem>();
        yield return new WaitForSeconds(3);
        _scorePanel.SetActive(false);
        if (!_isPassed)
        {
            _playerhealth.Damage(100f);
        }

        if (_isPassed)
        {
            _isAnswering = false;
            _startBossFight.SetBool("IsActive", true);
        }
    }

    public void ReadStringInput(string _answer)
    {
        _inputField.text = "";
        _inputField.ActivateInputField();
        _input = _answer;
        CorrectAnswer();
    }
}
