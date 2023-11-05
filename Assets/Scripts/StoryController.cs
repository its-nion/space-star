using UnityEngine;

public class StoryController : MonoBehaviour
{
    [Header("Gameobjects")]
    public Animator startAnimator;

    // private vars
    private int storyIndex = 0;

    // unity methods
    private void Update()
    {
        // Check if game was started
        if (Input.GetKeyDown(KeyCode.Space))
            startAnimator.SetBool("Started", true);
    }

    // public methods
    public void addStoryIndex()
    {
        storyIndex++;

        checkStoryIndex();
    }

    // private methods
    private void onIntroAnimationEnd()
    {
        startAnimator.enabled = false;

        addStoryIndex();
    }

    private void checkStoryIndex()
    {
        switch (storyIndex)
        {
            case 0: // Menu
                break;

            case 1: // Nach intro
                TypeWriter.Instance.StartDialog(
                    "Man, i really love this universe!",
                    null
                    );
                break;

            case 2:
                break;
        }
    }
}
