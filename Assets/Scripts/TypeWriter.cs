using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TypeWriter : MonoBehaviour
{
    [Header("Gameobjects")]
    [SerializeField] private TMP_Text _textBox;
    [SerializeField] private AudioSource _sfx;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovementController _pmc;

    [Header("Typewriter Settings")]
    [SerializeField] private float charactersPerSecond = 20;
    [SerializeField] [Range(0.1f, 0.5f)] private float sendDoneDelay = 0.25f;

    public static TypeWriter Instance { get; private set; }
    private string[] currentLines;
    private int _currentVisibleCharacterIndex;
    private WaitForSeconds _delay;
    private WaitForSeconds _endEventDelay;
    private static event Action EndFunction;

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

            _delay = new WaitForSeconds(1 / charactersPerSecond);

            _endEventDelay = new WaitForSeconds(sendDoneDelay);
        }
    }

    private void playSound()
    {
        //_sfx.Play();
    }

    public void StartDialog(string dialogText, Action endFunction)
    {
        _textBox.maxVisibleCharacters = 0;

        StartCoroutine(Dialog(dialogText, endFunction));
    }

    private IEnumerator Dialog(string text, Action endFunction)
    {
        _pmc.enabled = false;

        currentLines = text.Split('|');

        _animator.SetBool("Dialog", true);
        yield return new WaitForSeconds(0.35f);

        // Fuer jede line
        foreach (var l in currentLines)
        {
            // reset
            _textBox.maxVisibleCharacters = 0;
            _currentVisibleCharacterIndex = 0;

            // initiate
            _textBox.text = l;

            TMP_TextInfo textInfo = _textBox.textInfo;

            while (_currentVisibleCharacterIndex < textInfo.characterCount)
            {
                var lastCharacterIndex = textInfo.characterCount - 1;

                // Falls wir nicht beim letzten Buchstaben sind
                if (_currentVisibleCharacterIndex < lastCharacterIndex)
                {
                    char character = textInfo.characterInfo[_currentVisibleCharacterIndex].character;

                    _sfx.PlayOneShot(_sfx.clip, _sfx.volume);
                    _textBox.maxVisibleCharacters++;

                    yield return _delay;

                    _currentVisibleCharacterIndex++;
                }
                else if (_currentVisibleCharacterIndex >= lastCharacterIndex)
                {
                    // Falls wir beim letzten Buchstaben sind
                    _sfx.PlayOneShot(_sfx.clip, _sfx.volume);
                    _textBox.maxVisibleCharacters++;
                    _currentVisibleCharacterIndex++;

                    // Neue dialogseite
                    while (!Input.GetKeyDown(KeyCode.Space))
                    {
                        yield return null;
                    }
                }
            }
        }

        // alle lines durch
        _animator.SetBool("Dialog", false);

        yield return _endEventDelay;
        endFunction?.Invoke();

        _pmc.enabled = true;

        yield break;
    }

    private IEnumerator DisplayLine(string line)
    {
        TMP_TextInfo textInfo = _textBox.textInfo;

        while (_currentVisibleCharacterIndex < textInfo.characterCount + 1)
        {
            var lastCharacterIndex = textInfo.characterCount - 1;

            // Falls wir beim letzten Buchstaben sind
            if (_currentVisibleCharacterIndex >= lastCharacterIndex)
            {
                _textBox.maxVisibleCharacters++;
                yield break;
            }

            char character = textInfo.characterInfo[_currentVisibleCharacterIndex].character;

            // Neue dialogseite
            if (character == '|')
            {
                while (!Input.anyKeyDown)
                {
                    yield return null;
                }

                var newDialogText = _textBox.text.Substring(_currentVisibleCharacterIndex + 1);

                // reset
                _textBox.maxVisibleCharacters = 0;
                _currentVisibleCharacterIndex = 0;

                _textBox.text = newDialogText;
            }

            _textBox.maxVisibleCharacters++;

            yield return _delay;

            playSound();
            _currentVisibleCharacterIndex++;
        }
    }
}
