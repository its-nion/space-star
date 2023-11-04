using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public static Dialog Instance { get; private set; }

    [Header("Gameobjects")]
    [SerializeField] private TMP_Text text;

    [Header("Variables")]
    [SerializeField] private string[] lines;
    [SerializeField] private float  speed;

    private int _index;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void startDialog()
    {
        text.text = string.Empty;

        _index = 0;

        StartCoroutine(typeLine());
    }

    IEnumerator typeLine()
    {
        // Type each character 1 by 1

        foreach (var c in lines[_index].ToCharArray())
        {
            text.text += c;

            yield return new WaitForSeconds(speed);
        }
    }
}
