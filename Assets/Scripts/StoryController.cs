using TMPro;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    [Header("Gameobjects")]
    public Animator startAnimator;
    public GameObject missionUi;
    public TMP_Text missionText;
    public TypeWriter typeWriter;

    [SerializeField] private int storyIndex = 0;

    private void Start()
    {
        missionUi.SetActive(false);
    }

    private void Update()
    {
        // Check if game was started
        if (Input.GetKeyDown(KeyCode.Space))
            startAnimator.SetBool("Started", true);
    }



    private void onIntroAnimationEnd()
    {
        startAnimator.enabled = false;

        nextStep();
    }



    public void nextStep()
    {
        switch (storyIndex)
        {
            case 0: // Spieler hat intro animation gesehen
                storyIndex++;

                // Start Dialog
                typeWriter.StartDialog(
                    "1Ach, wie schön das Universum doch ist" +
                    "|1Ich bin so froh hier hergezogen zu sein" +
                    "|1Auf der Erde hat niemand meine Gesangskünste geschätzt" +
                    "|1Immer wurde ich mit Tomaten beworfen" +
                    "|1Aber hier bin ich allein!" +
                    "|1Hier kann ich singen so viel ich will!",
                    nextStep
                    );
                break;

            case 1: // Spieler hat Anfangsdialog gesehen
                // Mission anzeigen
                missionUi.SetActive(true);
                missionText.text = "- Singe ein bisschen";
                break;

            case 2: // Spieler hat erstes mal gesungen
                // Mission anzeigen
                missionUi.SetActive(true);
                missionText.text = "- Empfange die Nachricht im Cockpit";
                break;

            case 3: // Spieler hat erste Nachricht empfangen
                // Mission anzeigen
                missionUi.SetActive(true);
                missionText.text = "- Fang wieder an zu singen";
                break;

            case 4: // Spieler hat
                storyIndex++;

                // Mission anzeigen
                missionUi.SetActive(true);
                missionText.text = "- Fang wieder an zu singen";
                break;
        }
    }
    public void interactableItemTrigger(string type)
    {
        if (storyIndex == 1 && type == "Mikrofon") // Erstes singen
        {
            storyIndex++;

            typeWriter.StartDialog(
                    "1Lalalala." +
                    "|1Blaablibub" +
                    "|1Blablibubpeng" +
                    "|1Tereng" +
                    "|2Biiiip" +
                    "|1BlaaWAS" +
                    "|1Da scheint jemand mir eine Nachricht gesendet zu haben!",
                    nextStep
                    );
        }

        if (storyIndex == 2 && type == "Radio") // Erste Radio Nachricht nach singen
        {
            storyIndex++;

            typeWriter.StartDialog(
                    "2S?t?oo?o?oo?op" +
                    "|1Was" +
                    "|2S??t??o????p" +
                    "|1Eeeh" +
                    "|1Ok?" +
                    "|2Bzzzzzzzz",
                    nextStep
                    );
        }

        if (storyIndex == 3 && type == "Mikrofon") // Erstes singen
        {
            storyIndex++;

            typeWriter.StartDialog(
                    "1Jetzt wieder",
                    nextStep
                    );
        }
    }
}
