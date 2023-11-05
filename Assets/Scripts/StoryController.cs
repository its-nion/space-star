using TMPro;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    [Header("Gameobjects")]
    public Animator startAnimator;
    public Animator playerAnimator;
    public GameObject singParticleSystem;
    public GameObject ufos;
    public GameObject books;
    public GameObject lights;
    public GameObject missionUi;
    public GameObject alien;
    public GameObject credits;
    public TMP_Text missionText;
    public TypeWriter typeWriter;

    [SerializeField] private int storyIndex = 0;

    private void Start()
    {
        missionUi.SetActive(false);
        singParticleSystem.SetActive(false);
        ufos.SetActive(false);
        books.SetActive(false);
        lights.SetActive(true);
        alien.SetActive(false);
        credits.SetActive(false);
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
                storyIndex++;
                // Mission anzeigen
                missionUi.SetActive(true);
                missionText.text = "- Singe ein bisschen";
                break;

            case 3: // Spieler hat erstes mal gesungen
                storyIndex++;

                // Mission anzeigen
                missionUi.SetActive(true);
                missionText.text = "- Empfange die Nachricht im Cockpit";

                singParticleSystem.SetActive(false);
                playerAnimator.SetBool("isSinging", false);
                break;

            case 5: // Spieler hat erste Nachricht empfangen
                storyIndex++;

                // Mission anzeigen
                missionUi.SetActive(true);
                missionText.text = "- Fang wieder an zu singen";
                break;

            case 7: // Spieler hat 2tes mal gesungen
                storyIndex++;

                // Mission anzeigen
                missionUi.SetActive(true);
                missionText.text = "- Schaue nach was passiert ist";

                singParticleSystem.SetActive(false);
                playerAnimator.SetBool("isSinging", false);

                ufos.SetActive(true);
                break;

            case 9: // Spieler hat aliens im Fenster gesehen
                storyIndex++;

                // Mission anzeigen
                missionUi.SetActive(true);
                missionText.text = "- Hole dir in der Küche etwas zu essen";
                break;

            case 11: // Spieler war in kuche und Untersucht raumschiff
                storyIndex++;

                // Mission anzeigen
                missionUi.SetActive(true);
                missionText.text = "- Schaue wieder nach was passiert ist";

                books.SetActive(true);
                break;

            case 13: // Spieler war in kuche und Untersucht raumschiff
                storyIndex++;

                // Mission anzeigen
                missionUi.SetActive(true);
                missionText.text = "- Empfange deine Nachricht";
                break;

            case 15: // Spieler hat bei nachricht licht ausgemacht
                storyIndex++;

                // Mission anzeigen
                missionUi.SetActive(true);
                missionText.text = "- Gehe wieder Singen";
                break;

            case 17: // Spieler war in kuche und Untersucht raumschiff
                storyIndex++;

                // Mission anzeigen
                missionUi.SetActive(true);
                missionText.text = "- Schalte das Licht wieder an";

                alien.SetActive(true);
                break;

            case 19: // Ende
                // Mission anzeigen
                missionUi.SetActive(false);

                credits.SetActive(true);
                break;
        }
    }
    public void interactableItemTrigger(string type)
    {
        if (storyIndex == 2 && type == "Mikrofon") // Erstes singen
        {
            storyIndex++;

            playerAnimator.SetBool("isSinging", true);
            singParticleSystem.SetActive(true);

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

            return;
        }

        if (storyIndex == 4 && type == "Radio") // Erste Radio Nachricht nach singen
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

            return;
        }

        if (storyIndex == 6 && type == "Mikrofon") // Zweites Singen
        {
            storyIndex++;

            playerAnimator.SetBool("isSinging", true);
            singParticleSystem.SetActive(true);

            typeWriter.StartDialog(
                    "1Jetzt wieder!" +
                    "|1LalalALA" +
                    "|3BOOOOOM" +
                    "|1Was war das?" +
                    "|1Ich sollte mal nachschauen!",
                    nextStep
                    );

            return;
        }

        if (storyIndex == 8 && type == "Fenster") // Aliens im Fenster
        {
            storyIndex++;

            typeWriter.StartDialog(
                    "1Was sind das denn für Dinger" +
                    "|3uiiiiiiiiiiiii" +
                    "|1Und was ist das für ein Geräusch?" +
                    "|1Das macht mich irgendwie hungrig" +
                    "|1Ich hol mir mal was in der Küche",
                    nextStep
                    );

            return;
        }

        if (storyIndex == 10 && type == "Kuche") // Reden in der Kuche
        {
            storyIndex++;

            typeWriter.StartDialog(
                    "1WAS SEH ICH HIER" +
                    "|1Ich hab leider nur noch Bier im Kühlschrank" +
                    "|1Und gerade bin ich auf Entzug" +
                    "|3BADUUM" +
                    "|1WAS" +
                    "|1NICHT SCHON WIEDER" +
                    "|1<size=40>ich hab langsam das gefühl ich bin nicht allein</size>",
                    nextStep
                    );

            return;
        }

        if (storyIndex == 12 && type == "Bucherregal") // Runtergefallenes Buch entdeckt
        {
            storyIndex++;

            typeWriter.StartDialog(
                    "1Wieso liegt das auf dem Boden?" +
                    "|1Das lag vorhin noch nicht hier" +
                    "|1... nicht das ich Bücher lesen würde" +
                    "|1Ist heute Halloween?" +
                    "|1Ich hab Aaaaaaangst" +
                    "|2Biiiip" +
                    "|1<size=80>AAAAAAAAHHHH</size>" +
                    "|1Ooh, das ist nur eine neue Nachricht",
                    nextStep
                    );

            return;
        }

        if (storyIndex == 14 && type == "Radio") // Letztes mal beim Radio
        {
            storyIndex++;

            typeWriter.StartDialog(
                    "1Wie lautet die Nachricht?" +
                    "|2?Ha?r???rg??r" +
                    "|1Was soll dass denn jetzt bitte schon wieder" +
                    "|1Mir reichts!" +
                    "|1Ich gehe wieder singen!",
                    nextStep
                    );

            return;
        }

        if (storyIndex == 16 && type == "Mikrofon") // Beim Lichtschalter
        {
            storyIndex++;

            lights.SetActive(false);

            typeWriter.StartDialog(
                    "3Peng" +
                    "|1Blinzel ich gerade sehr lange oder ist das Licht ausgegangen?",
                    nextStep
                    );

            return;
        }

        if (storyIndex == 18 && type == "Lichtschalter") // Beim Lichtschalter
        {
            storyIndex++;

            lights.SetActive(true);

            typeWriter.StartDialog(
                    "1AAAAAAAAAHHHHHH?" +
                    "|4HÖR AUF ZU SCHREIEN" +
                    "|1Was?" +
                    "|4ICH KANN DEIN GESANG NICHT MEHR AUSSTEHEN" +
                    "|4DENKST DU, DU BIST ALLEIN HIER IM WELTALL?",
                    nextStep
                    );

            return;
        }

        if (type == "Lichtschalter") // Beim Lichtschalter
        {
            typeWriter.StartDialog(
                    "1Ich..." +
                    "|1...hab grad keine Lust den zu betätigen!",
                    null
                    );

            return;
        }

        if (type == "Kuche") // Beim Lichtschalter
        {
            typeWriter.StartDialog(
                    "1Sehe ich aus als wäre ich hungrig?",
                    null
                    );

            return;
        }

        if (type == "Mikrofon") // Beim Lichtschalter
        {
            typeWriter.StartDialog(
                    "1Ich erspare dir mal meinen wunderschönen Gesang",
                    null
                    );

            return;
        }
        
        if (type == "Radio") // Beim Lichtschalter
        {
            typeWriter.StartDialog(
                    "2Keine neuen Nachrichten",
                    null
                    );

            return;
        }

        if (type == "Bucherregal") // Beim Lichtschalter
        {
            typeWriter.StartDialog(
                    "1Noch nie angefasst" +
                    "|1Ich weiß nichtmal warum das hier steht!",
                    null
                    );

            return;
        }

        if (type == "Fenster") // Beim Lichtschalter
        {
            typeWriter.StartDialog(
                    "1Wunderschön, nicht wahr?",
                    null
                    );

            return;
        }
    }
}
