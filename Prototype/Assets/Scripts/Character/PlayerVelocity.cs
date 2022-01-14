using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

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
	[SerializeField] private float airDashPower = 10f;
	[SerializeField] private float dashCooldown = 2f;
	[SerializeField] private float rollPower = 10f;
	[SerializeField] private float rollCooldown = 2f;
	[SerializeField] private float knockbackCooldown = 0.15f;
	[SerializeField] private int maxAirJump = 1;
	[SerializeField] private PlayerAnimator playerAnimator;

	[SerializeField] private Vector2 standingColliderSize;
	[SerializeField] private Vector2 rollingColliderSize;
	[SerializeField] private Vector2 standingColliderOffset;
	[SerializeField] private Vector2 rollingColliderOffset;

	[SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioClip runningSfx;
	[SerializeField] private AudioClip jumpSfx;
	[SerializeField] private AudioClip dashSfx;
	[SerializeField] private AudioClip slashSfx;

	[SerializeField] ParticleSystem dustEffect; 

	private int airJumpCount;
	private int dashCount;
	private float lastDashTime;
	private float lastRollTime;
	private float lastKnockbackTime;
	private float lastHorizontalVelocity;
	private float tempDashPower;
	private bool isDashing;
	private bool isRolling;
	private bool isInAir;
	private bool isInKnockback;
	private bool isMoving;

	private float gravity;
	private float maxJumpVelocity;
	private float minJumpVelocity;
	private Vector3 velocity;
	private Vector3 oldVelocity;
	private float velocityXSmoothing;

	private Timeline time;
	private Movement playerMovement;
	private PlayerInput playerInput;
	private PlayerStatManager playerStat;
	private Vector2 directionalInput;

	void Start()
	{
		playerMovement = GetComponent<Movement>();
		playerInput = GetComponent<PlayerInput>();
		playerStat = GetComponent<PlayerStatManager>();
		time = GetComponent<Timeline>();


		// see suvat calculations; s = ut + 1/2at^2, v^2 = u^2 + 2at, where u=0, scalar looking at only y dir
		gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
	}

	void Update()
	{
		CalculateVelocity();
		// checkKnockback();
		// r = r0 + 1/2(v+v0)t, note Vector version used here
		// displacement = 1/2(v+v0)t since the playerMovementController uses Translate which moves from r0

		Vector3 displacement = (velocity + oldVelocity) * 0.5f * time.deltaTime;
		// Move player using movement controller which checks for collisions then applies correct transform (displacement) translation
		
		playerMovement.Move(displacement, directionalInput);

		// check if player is running
		if(directionalInput.x != 0)
        {
			isMoving = true;
        } 
		else
        {
			isMoving = false;
        }

        // play running sfx
        if (isMoving && playerMovement.collisionDirection.below)
        {
			if (!audioSource.isPlaying)
			{
				audioSource.clip = runningSfx;
				audioSource.Play();
			}
		}
		else
        {
			audioSource.Stop();
        }

		bool verticalCollision = playerMovement.collisionDirection.above || playerMovement.collisionDirection.below;

		if (verticalCollision)
		{
			if (playerMovement.slidingDownMaxSlope)
			{
				velocity.y += playerMovement.collisionAngle.slopeNormal.y * -gravity * time.deltaTime;
			}
			else
			{
				velocity.y = 0;
			}
		}

		if (playerMovement.collisionDirection.below)
        {
			airJumpCount = 0;
			dashCount = 0;
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
        if (isDashing)
        {
			smoothTime = accelerationTimeAirborne;
        }
		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, smoothTime / time.timeScale);

		if (velocity.x != 0)
        {
			lastHorizontalVelocity = velocity.x;
        }

		velocity.y += gravity * time.deltaTime;

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
		//check apakah knockback udh selesai
		
		directionalInput = input;
	}

	private void enableMovement()
	{
		isInKnockback = false;
		playerMovement.lockedFlip = false;
		playerInput.enabled = true;
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

		createDust(true);
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

		// test sfx
		if (jumpSfx != null)
		{
			SoundManager.Instance.PlaySFX(jumpSfx, 0.2f);
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
	public void OnDashInputDown(float dashDirectionX = 0 , float dashDirectionY = 0, bool normalDash = false)
    {
        
		if (time.time - lastDashTime < dashCooldown && normalDash)
		{
			return;
		}
		//set old velocity to 0 so it doesnt affect the dash
		
		playerInput.enabled = false;
		//directionalInput.x = 0;
		//cek ground collision nya ku ganti sedikit karena kalau misalnya di ground dia bisa dash dalam bentuk roll , tapi dengan power se fraksi air dash atau sama
		if (dashCount == 0)
        {
			tempDashPower = dashPower;
			dashCount += 1;

		}
		else
        {
			tempDashPower = airDashPower ;
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
	    lastDashTime = time.time;
		
		airJumpCount++;

        // test sfx
        

        if (normalDash == false)
        {
			SoundManager.Instance.PlaySFX(slashSfx);
		} else
        {
			playerStat.setInvulnerable();
			SoundManager.Instance.PlaySFX(dashSfx);
        }
		createDust(false);
	}

	public void knockback(float DirectionX = 0, float DirectionY = 0)
    {
		directionalInput.x = 0;
		playerAnimator.SetKnockback();
		playerInput.enabled = false;
		isInKnockback = true;
		lastKnockbackTime = time.time;
		playerMovement.lockedFlip = true;
		velocity = new Vector2(DirectionX, DirectionY);
	}
	private void checkKnockback()
    {

		Debug.Log(isInKnockback);
		if (isInKnockback && (time.time - lastKnockbackTime) > knockbackCooldown)
		{

			isInKnockback = false;
			playerInput.enabled = true;
		}
	}
	

	private void ResetDashing()
    {
		
		playerInput.enabled = true;
		isDashing = false;

		playerMovement.lockedFlip = false;
	}

	public void Roll()
    {
		if (time.time - lastRollTime < rollCooldown)
		{
			return;
		}

		if (!playerMovement.isGrounded)
		{
			return;
		}

		isRolling = true;
		var rollDirection = rollPower;

		if (lastHorizontalVelocity < 0)
		{
			rollDirection = -rollPower;
		}

		velocity = new Vector2(rollDirection, velocity.y);
		lastRollTime = time.time;

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
		playerAnimator.SetSpeedY(velocity.y);
		playerAnimator.SetRolling(isRolling);
	}

	private void createDust(bool isJump)
    {

		dustEffect.Stop(true , ParticleSystemStopBehavior.StopEmittingAndClear);
		if (!dustEffect.isPlaying)
        {
            if (isJump)
            {

				var main = dustEffect.main;
				main.duration = 0.05f;
			}
            else
            {

				var main = dustEffect.main;
				main.duration = 0.2f;
			}
			dustEffect.Play();
		}
    }
}
