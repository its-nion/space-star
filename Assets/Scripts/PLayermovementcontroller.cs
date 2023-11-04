using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1.0f;

    private Vector2 movement;

    private Animator animator;
    private Transform characterTransform;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterTransform = transform; // Get a reference to the character's Transform.
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Apply dead zone
        if (Mathf.Abs(horizontalInput) < 0.1f)
        {
            horizontalInput = 0f;
        }

        movement = new Vector2(Input.GetAxis("Horizontal"), 0).normalized;
        animator.SetFloat("Speed", Mathf.Abs(movement.magnitude * movementSpeed));

        if (movement.x < 0)
        {
            characterTransform.localScale = new Vector3(-1, 1, 1); // Spiegel die Figur nach links
        }
        else if (movement.x > 0)
        {
            characterTransform.localScale = new Vector3(1, 1, 1); // Stelle die Skalierung auf die ursprüngliche Richtung zurück
        }
    }

    private void FixedUpdate()
    {
        if (movement != Vector2.zero)
        {
            var xMovement = movement.x * movementSpeed * Time.deltaTime;
            characterTransform.Translate(new Vector3(xMovement, 0), Space.World);
        }
    }
}
