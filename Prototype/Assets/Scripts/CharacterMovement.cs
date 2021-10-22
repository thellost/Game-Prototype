using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Character2DController controller;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
