using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerVelocity))]
public class PlayerInput : MonoBehaviour
{
	private TimeManager timeManager;
	private PlayerVelocity playerVelocity;

	private bool isMapOpened;

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
		
		if (Input.GetKeyDown(KeyCode.LeftControl))
        {
			playerVelocity.Roll();
        }
		
		if (Input.GetKeyDown(KeyCode.X))
		{
			Debug.Log("test slow");
			timeManager.DoSlowMotion();
		}

		if (Input.GetKeyDown(KeyCode.M))
        {
            if (isMapOpened)
            {
				CloseMap();
            }

            else
            {
				OpenMap();
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
			playerVelocity.knockback();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
			SceneManager.LoadScene("Augment");
        }
	}

	private void OpenMap()
    {
		isMapOpened = true;
		SceneManager.LoadScene("Map", LoadSceneMode.Additive);
	}

	private void CloseMap()
    {
		isMapOpened = false;
		SceneManager.UnloadSceneAsync("Map");
	}
}