using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriter : MonoBehaviour
{
    [Header("Gameobjects")]
    [SerializeField] private TMP_Text _textBox;
    [SerializeField] private AudioSource _sfx;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovementController _pmc;
    [SerializeField] private RawImage _rImg;

    [Header("Textures")]
    [SerializeField] private Texture _astronaut;
    [SerializeField] private Texture _radio;
    [SerializeField] private Texture _questionMark;
    [SerializeField] private Texture _alien;

    [Header("Typewriter Settings")]
    [SerializeField] private float charactersPerSecond = 20;
    [SerializeField] [Range(0.1f, 0.5f)] private float sendDoneDelay = 0.25f;

    public bool isAvailable = true;

    private string[] currentLines;
    private int _currentVisibleCharacterIndex;
    private WaitForSeconds _delay;
    private WaitForSeconds _endEventDelay;
    private static event Action EndFunction;

    private void Awake()
    {
        _delay = new WaitForSeconds(1 / charactersPerSecond);

        _endEventDelay = new WaitForSeconds(sendDoneDelay);
    }

    private void playSound()
    {
        //_sfx.Play();
    }

    private void switchRawImageToCharacter(char c)
    {
        switch (c)
        {
            case '1': // Astronaut
                _rImg.texture = _astronaut;
                break;
            case '2': // Radio
                _rImg.texture = _radio;
                break;
            case '3': // Fragezeichen
                _rImg.texture = _questionMark;
                break;
            case '4': // Alien
                _rImg.texture = _alien;
                break;
            default:
                _rImg.texture = _astronaut;
                break;
        }
    }

    public void StartDialog(string dialogText, Action endFunction)
    {
        _textBox.maxVisibleCharacters = 0;

        StartCoroutine(Dialog(dialogText, endFunction));
    }

    private IEnumerator Dialog(string text, Action endFunction)
    {
        isAvailable = false;
        _pmc.enabled = false;

        currentLines = text.Split('|');

        if (currentLines[0][0] == '1' | currentLines[0][0] == '2' | currentLines[0][0] == '3' | currentLines[0][0] == '4')
        {
            switchRawImageToCharacter(currentLines[0][0]);
            currentLines[0] = currentLines[0].Substring(1);
        }

        _animator.SetBool("Dialog", true);
        yield return new WaitForSeconds(0.35f);

        // Fuer jede line
        foreach (var l in currentLines)
        {
            // reset
            _textBox.maxVisibleCharacters = 0;
            _currentVisibleCharacterIndex = 0;

            if (l[0] == '1' | l[0] == '2' | l[0] == '3' | l[0] == '4')
            {
                switchRawImageToCharacter(l[0]);
                _textBox.text = l.Substring(1);
            }
            else
            {
                _textBox.text = l;
            }

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
        isAvailable = true;

        yield break;
    }
}
