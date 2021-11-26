using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(PlayerInput))]
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
	[SerializeField] private float rollPower = 10f;
	[SerializeField] private float rollCooldown = 2f;
	[SerializeField] private int maxAirJump = 1;
	[SerializeField] private PlayerAnimator playerAnimator;
	[SerializeField] private Vector2 standingColliderSize;
	[SerializeField] private Vector2 rollingColliderSize;
	[SerializeField] private Vector2 standingColliderOffset;
	[SerializeField] private Vector2 rollingColliderOffset;
	
	private int airJumpCount;
	private int dashCount;
	private float lastDashTime;
	private float lastRollTime;
	private float lastHorizontalVelocity;
	private float tempDashPower;
	private bool isDashing;
	private bool isRolling;
	private bool isInAir;

	private float gravity;
	private float maxJumpVelocity;
	private float minJumpVelocity;
	private Vector3 velocity;
	private Vector3 oldVelocity;
	private float velocityXSmoothing;

	private Movement playerMovement;
	private PlayerInput playerInput;
	private Vector2 directionalInput;

	void Start()
	{
		playerMovement = GetComponent<Movement>();
		playerInput = GetComponent<PlayerInput>();
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

		HandleAnimation();
	}

	//check apakah player ada di udara apa ga , ini setiap update
	

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

		velocity.y += gravity * Time.deltaTime;

		// if (!isDashing)
        // {
		//	velocity.y += gravity * Time.deltaTime;
		// } else
        // {
			// velocity.y = 0;
        // }
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


	//kutambahin optional argument yang berguna untuk attack dash
	public void OnDashInputDown(float dashDirectionX = 0 , float dashDirectionY = 0)
    {
		playerInput.enabled = false;
		if (Time.time - lastDashTime < dashCooldown)
		{
			return;
		}

		//cek ground collision nya ku ganti sedikit karena kalau misalnya di ground dia bisa dash dalam bentuk roll , tapi dengan power se fraksi air dash atau sama
		if (true)
        {
			tempDashPower = dashPower;
        }
        else if(!playerMovement.collisionDirection.below )
        {
			dashCount += 1;
			tempDashPower = dashPower;
        }

		isDashing = true;

		var dashDirection = tempDashPower;


		
		if (lastHorizontalVelocity < 0)
        {
			dashDirection = -tempDashPower;
        }

		
		velocity = new Vector2(dashDirection, velocity.y);

		//MENGUBAH ARAH DASH jika argument tidak default
		if (dashDirectionX != 0 || dashDirectionY != 0)
		{
			
			velocity = Vector2.one * tempDashPower;
			velocity = new Vector2(dashDirectionX * velocity.x, velocity.y * dashDirectionY);
		}
		lastDashTime = Time.time;
		airJumpCount++;
    }

	private void ResetDashing()
    {
		playerInput.enabled = true;
		isDashing = false;
    }

	public void Roll()
    {
		if (Time.time - lastRollTime < rollCooldown)
		{
			return;
		}

		if (!playerMovement.isGrounded)
		{
			return;
		}

		isRolling = true;
		Invoke("ResetRolling", 0.3f);

		var rollDirection = rollPower;

		if (lastHorizontalVelocity < 0)
		{
			rollDirection = -rollPower;
		}

		velocity = new Vector2(rollDirection, velocity.y);
		lastRollTime = Time.time;

		playerMovement.boxCollider.size = rollingColliderSize;
		playerMovement.boxCollider.offset = rollingColliderOffset;
	}

	private void ResetRolling()
	{
		isRolling = false;
		playerMovement.boxCollider.size = standingColliderSize;
		playerMovement.boxCollider.offset = standingColliderOffset;
	}

	private void HandleAnimation()
    {
		playerAnimator.SetDashing(isDashing);
		playerAnimator.SetJumping(!playerMovement.isGrounded);
		// playerAnimator.SetRunning(directionalInput.x != 0);
		playerAnimator.SetSpeedX(Mathf.Abs(directionalInput.x));
		playerAnimator.SetSpeedY(Mathf.Abs(velocity.y));
		playerAnimator.SetRolling(isRolling);
	}
}
