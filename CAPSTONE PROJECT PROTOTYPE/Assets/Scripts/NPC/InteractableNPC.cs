using UnityEngine;

public class InteractableNPC : MonoBehaviour
{

    private bool isInteractable;
    public GameObject dialouge;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isInteractable)
        {
            Debug.Log("Clicked F");
            dialouge.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isInteractable = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isInteractable = false;
        dialouge.SetActive(false);
    }
}
