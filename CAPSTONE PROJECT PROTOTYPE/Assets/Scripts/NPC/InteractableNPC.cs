using UnityEngine;

public class InteractableNPC : MonoBehaviour
{

    private bool _isInteractable;
    public GameObject _dialouge;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _isInteractable)
        {
            Debug.Log("Clicked F");
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
