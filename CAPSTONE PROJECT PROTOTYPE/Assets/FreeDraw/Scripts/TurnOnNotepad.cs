using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TurnOnNotepad : MonoBehaviour
{
    public GameObject _notepad;
    public CinemachineVirtualCamera _notepadCamera;
    public CinemachineVirtualCamera _playerCamera;
    private bool _isUsingNotepad;

    private void Start()
    {
        _isUsingNotepad = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (_isUsingNotepad)
            {
                Debug.Log("_notUsing");
                _notepadCamera.Priority = 0;
                _playerCamera.Priority = 1;
                _notepad.SetActive(false);
                _isUsingNotepad = false;
            }
            else
            {
                Debug.Log("_isUsing");
                _playerCamera.Priority = 0;
                _notepadCamera.Priority = 1;
                _notepad.SetActive(true);
                _isUsingNotepad = true;
            }
        }
    }
}
