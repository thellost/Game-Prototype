using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class PlayerVelocity : MonoBehaviour
{

	[SerializeField] private float moveSpeed = 10;
	[SerializeField] private float maxJumpHeight = 4;
	[SerializeField] private float minJumpHeight = 1;
	[SerializeField] private float timeToJumpApex = .4f;
	[SerializeField] private float accelerationTimeAirborne = .2f;
	[SerializeField] private float accelerationTimeGrounded = .1f;
	[SerializeField] private float forceFallSpeed = 20;
	[SerializeField] private float dashPower = 10f;
	[SerializeField] private float dashCooldown = 2f;
	[SerializeField] private int maxAirJump = 1;
	

	private int airJumpCount;
	private float lastDashTime;
	private float lastHorizontalVelocity;
	private bool isDashing;

	//[SerializeField] private Vector2 wallJump;
	//[SerializeField] private Vector2 wallJumpClimb;
	//[SerializeField] private Vector2 wallLeapOff;

	//[SerializeField] private float wallSlideSpeedMax = 3;
	//[SerializeField] private float wallStickTime = .25f;

	private float timeToWallUnstick;
	private float gravity;
	private float maxJumpVelocity;
	private float minJumpVelocity;
	private Vector3 velocity;
	private Vector3 oldVelocity;
	private float velocityXSmoothing;

	private Movement playerMovement;

	private Vector2 directionalInput;
	private bool wallContact;
	private int wallDirX;

	void Start()
	{
		playerMovement = GetComponent<Movement>();

		// see suvat calculations; s = ut + 1/2at^2, v^2 = u^2 + 2at, where u=0, scalar looking at only y dir
		gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
	}

	void Update()
	{
		CalculateVelocity();
		

		// r = r0 + 1/2(v+v0)t, note Vector version used here
		// displacement = 1/2(v+v0)t since the playerMovementController uses Translate which moves from r0
		Vector3 displacement = (velocity + oldVelocity) * 0.5f * Time.deltaTime;
		// Move player using movement controller which checks for collisions then applies correct transform (displacement) translation
		playerMovement.Move(displacement, directionalInput);

		bool verticalCollision = playerMovement.collisionDirection.above || playerMovement.collisionDirection.below;

		if (verticalCollision)
		{
			if (playerMovement.slidingDownMaxSlope)
			{
				velocity.y += playerMovement.collisionAngle.slopeNormal.y * -gravity * Time.deltaTime;
			}
			else
			{
				velocity.y = 0;
			}
		}

		if (playerMovement.collisionDirection.below)
        {
			airJumpCount = 0;
        }
	}

	public void CalculateVelocity()
	{
		// suvat; s = ut, note a=0
		float targetVelocityX = directionalInput.x * moveSpeed;
		oldVelocity = velocity;
		// ms when player is on the ground faster vs. in air
		float smoothTime = (playerMovement.collisionDirection.below) ? accelerationTimeGrounded : accelerationTimeAirborne;
		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, smoothTime);

		if (velocity.x != 0)
        {
			lastHorizontalVelocity = velocity.x;
        }

		if (!isDashing)
        {
			velocity.y += gravity * Time.deltaTime;
		} else
        {
			velocity.y = 0;
        }
	}


	/* Public Functions used by PlayerInput script */

	/// <summary>
	/// Handle horizontal movement input
	/// </summary>
	public void SetDirectionalInput(Vector2 input)
	{
		directionalInput = input;
	}

	/// <summary>
	/// Handle jumps
	/// </summary>
	public void OnJumpInputDown()
	{
		if (playerMovement.collisionDirection.below)
		{
			Jump();
		} 
		else if(airJumpCount < maxAirJump)
        {
			Jump();
			airJumpCount++;
        }
	}

	private void Jump()
    {
		if (playerMovement.slidingDownMaxSlope)
		{
			// Jumping away from max slope dir
			if (directionalInput.x != -Mathf.Sign(playerMovement.collisionAngle.slopeNormal.x))
			{
				velocity.y = maxJumpVelocity * playerMovement.collisionAngle.slopeNormal.y;
				velocity.x = maxJumpVelocity * playerMovement.collisionAngle.slopeNormal.x;
			}
		}
		else
		{
			velocity.y = maxJumpVelocity;
		}
	}

	/// <summary>
	/// Handle not fully commited jumps - allow for mini jumps
	/// </summary>
	public void OnJumpInputUp()
	{
		if (velocity.y > minJumpVelocity)
		{
			velocity.y = minJumpVelocity;
		}
	}

	/// <summary>
	/// Handle down direction - force fall
	/// </summary>
	public void OnFallInputDown()
	{
		if (!playerMovement.collisionDirection.below)
		{
			velocity.y = -forceFallSpeed;
			playerMovement.forceFall = true;
		}
	}

	public void OnDashInputDown()
    {
		if (playerMovement.collisionDirection.below)
        {
			return;
        }

		isDashing = true;
		Invoke("ResetDashing", 0.3f);

		if (Time.time - lastDashTime < dashCooldown)
        {
			return;
        }

		var dashDirection = dashPower;

		if (lastHorizontalVelocity < 0)
        {
			dashDirection = -dashPower;
        }

		velocity = new Vector2(dashDirection, velocity.y);
		lastDashTime = Time.time;
    }

	private void ResetDashing()
    {
		isDashing = false;
    }
}
