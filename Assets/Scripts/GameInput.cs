using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {

	public event EventHandler OnJumpAction;
	public event EventHandler OnTransformAction;


	private PlayerInputActions playerInputActions;

	void Awake() {
		playerInputActions = new PlayerInputActions();
		playerInputActions.Player.Enable();

		playerInputActions.Player.Jump.performed += Jump_performed;
		playerInputActions.Player.Transform.performed += Transform_performed;
	}

	private void Transform_performed(InputAction.CallbackContext context) {
		OnTransformAction?.Invoke(this, EventArgs.Empty);
	}

	private void Jump_performed(InputAction.CallbackContext context) {
		OnJumpAction?.Invoke(this, EventArgs.Empty);
	}

	public Vector2 GetMovementVector() {

		Vector2 movementVector = playerInputActions.Player.Movement.ReadValue<Vector2>();

		return movementVector;

	}


	public bool GetJump() {
		return playerInputActions.Player.Jump.ReadValue<bool>();
	}


}
