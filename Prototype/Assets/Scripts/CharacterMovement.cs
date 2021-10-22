using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Character2DController controller;
    public float runSpeed = 40f;

    float horizontalMove = 0f;

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }
}
