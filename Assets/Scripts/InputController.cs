using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TypeWriter.Instance.StartDialog("Hallo meine Freunde, und die die sich nur als solche ausgeben!|Ich liebe euch ein bisschen!|Hallo ELIAS DER GRO?EEE", debugLogTest);
        }
    }

    public void debugLogTest()
    {
        Debug.Log("TEST SUCCESSFULL!");
    }
}
