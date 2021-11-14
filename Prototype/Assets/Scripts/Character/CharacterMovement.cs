using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

	public Character2DController controller;
	public TimeManager timeManager;
	public Animator animator;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	float verticalMove = 0f;
	bool jump = false;
	bool crouch = false;
	Rigidbody2D rb;
    private void Start()
    {
		rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
	{

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		verticalMove = rb.velocity.y;
		Debug.Log(verticalMove);
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
		animator.SetFloat("SpeedY", Mathf.Abs(horizontalMove));
		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator.SetBool("IsJumping", true);
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		}
		else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}

		if (Input.GetKeyDown(KeyCode.X))
		{
			timeManager.DoSlowMotion();
		}

	}

	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);
	}

	public void OnCrouching(bool isCrouching)
	{
		animator.SetBool("IsCrouching", isCrouching);
	}

	void FixedUpdate()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}
