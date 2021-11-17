using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerVelocity))]
public class PlayerInput : MonoBehaviour
{
	public Animator animator;
	private TimeManager timeManager;
	private PlayerVelocity playerVelocity;


	void Start()
	{
		playerVelocity = GetComponent<PlayerVelocity>();
		timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
	}

	void Update()
	{
		Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		playerVelocity.SetDirectionalInput(directionalInput);

		if (Input.GetKeyDown(KeyCode.W))
		{
			playerVelocity.OnJumpInputDown();
		}

		if (Input.GetKeyUp(KeyCode.W))
		{
			playerVelocity.OnJumpInputUp();
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			playerVelocity.OnFallInputDown();
		}

		if (Input.GetKeyDown(KeyCode.LeftShift))
        {
			playerVelocity.OnDashInputDown();
        }
		if (Input.GetKeyDown(KeyCode.X))
		{
			Debug.Log("test slow");
			timeManager.DoSlowMotion();
		}
	}
}