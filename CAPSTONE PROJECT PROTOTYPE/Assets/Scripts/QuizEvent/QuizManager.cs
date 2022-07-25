
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public GameObject _questionBox;
    public GameObject _textObj;
    private TMP_Text _myText;
    [NonReorderable] public List<QnA> _qna = new List<QnA>(); //NonReorderable attribute fix the visual bug.
    private string _input;
    private int _currentQuestionIndex;
    public bool _isPassed = false;
    private PlayerHealthSystem _player;

    private void Start()
    {
        _myText = _textObj.GetComponent<TMP_Text>();
        RandomizeQuestion();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthSystem>();
    }

    public void RandomizeQuestion()
    {
        _currentQuestionIndex = Random.Range(0, _qna.Count);
        _myText.text = _qna[_currentQuestionIndex]._question;
    }

    public void ReadStringInput(string _answer)
    {
        Debug.Log("Is entered");
        _input = _answer;
        AnswerIsCorrect();
    }

    public void AnswerIsCorrect()
    {
        if (_input == _qna[_currentQuestionIndex]._correctAnswer)
        {
            if (_qna.Count == 1)
            {
                _isPassed = true;
                _questionBox.SetActive(false);
            }
            else
            {
                _qna.RemoveAt(_currentQuestionIndex);
                RandomizeQuestion();
            }
        }
        else
        {
            _player.Damage(50f);
        }
    }
}
