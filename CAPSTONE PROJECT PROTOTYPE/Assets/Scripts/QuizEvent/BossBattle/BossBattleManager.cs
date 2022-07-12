using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossBattleManager : MonoBehaviour
{
    //Boss UI
    [SerializeField] private GameObject _bossUI;
    private TMP_Text _questionText;
    [NonReorderable] public List<QnA> _qna = new List<QnA>();
    private bool _isPassed = false;
    private int _currentQuestionIndex;

    //Properties
    [SerializeField] private int _maximumNumberOfQuestions;
    private int _questionCounter = 1;
    private int _correctAnswerCounter;
    private int _damageBonusMultiplier = 0;
    private string _input;


    void Start()
    {
        _questionText = _bossUI.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();

        RandomizeQuestion();
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
                _correctAnswerCounter++;
            }
            _questionCounter++;
            _qna.RemoveAt(_currentQuestionIndex);
            RandomizeQuestion();
        }

        if (_questionCounter == _maximumNumberOfQuestions)
        {
            MaximumQuestionReached();
        }
    }

    private void MaximumQuestionReached()
    {
        if (_correctAnswerCounter >= (Mathf.Abs(_maximumNumberOfQuestions / 2)))
        {
            _isPassed = true;
            Debug.Log("You Win");
            _bossUI.SetActive(false);
        }
        else
        {
            _isPassed = false;
            Debug.Log("You failed");
            _bossUI.SetActive(false);
        }
    }

    public void ReadStringInput(string _answer)
    {
        Debug.Log("Is entered");
        _input = _answer;
        CorrectAnswer();
    }
}
