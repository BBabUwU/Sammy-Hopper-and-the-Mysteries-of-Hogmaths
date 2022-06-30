using UnityEngine;
using System.Collections;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI _textComponent;
    public string[] _lines;
    public float _textSpeed;
    public int _index; //to track which character we are.

    private void Update()
    {
        SkipLine();
    }

    public void StartDialogue()
    {
        _index = 0;
        StartCoroutine(TypeLine());
    }

    private void OnEnable()
    {
        _textComponent.text = string.Empty;
        StartDialogue();
    }

    void SkipLine()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_textComponent.text == _lines[_index])
            {
                NextLine();
            }

            else
            {
                StopAllCoroutines();
                _textComponent.text = _lines[_index];
            }

        }
    }

    IEnumerator TypeLine()
    {
        //Types each character one by one.
        foreach (char c in _lines[_index].ToCharArray())
        {
            _textComponent.text += c;
            yield return new WaitForSeconds(_textSpeed);
        }
    }

    void NextLine()
    {
        if (_index < _lines.Length - 1)
        {
            _index++;
            _textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
