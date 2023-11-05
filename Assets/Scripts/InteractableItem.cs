using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public string type;
    public GameObject text;
    public StoryController storyController;
    public TypeWriter typeWriter;

    private bool canInteract = false;

    void Start()
    {
        text.SetActive(false);
    }

    private void Update()
    {
        if (canInteract)
        {
            var dialogAvailable = typeWriter.isAvailable;

            if (dialogAvailable)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    text.SetActive(false);
                    canInteract = false;

                    storyController.interactableItemTrigger(type);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        text.SetActive(true);
        canInteract = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        text.SetActive(false);
        canInteract = false;
    }
}
