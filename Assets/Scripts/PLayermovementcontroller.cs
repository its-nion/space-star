using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayermovementcontroller : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1.0f;

    private Vector2 movement;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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

        bool flipped = movement.x < 0;
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));
    }

    private void FixedUpdate()
    {
        if (movement != Vector2.zero)
        {
            var xMovement = movement.x * movementSpeed * Time.deltaTime;
            transform.Translate(new Vector3(xMovement, 0), Space.World);
        }

    }
}
