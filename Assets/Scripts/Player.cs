using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {



	[SerializeField] private PhysicsMaterial2D bouncyMaterial;
	[SerializeField] private PhysicsMaterial2D normalMaterial;
	[SerializeField] private Collider2D ballCollider;
	[SerializeField] private PlayerVisual playerVisual;
	[SerializeField] private GameInput gameInput;
	[SerializeField] private float movementSpeed = 6f;
	[SerializeField] private float jumpForce = 5f;


	private Rigidbody2D rigidBody;
	private bool canClimb;
	private bool isClimbing;

	private bool isBall = false;


	private bool isGrounded;

	void Awake() {

		rigidBody = GetComponent<Rigidbody2D>();

		gameInput.OnJumpAction += GameInput_OnJumpAction;
		gameInput.OnTransformAction += GameInput_OnTransformAction;

		ballCollider.sharedMaterial = normalMaterial;


	}

	private void GameInput_OnTransformAction(object sender, EventArgs e) {
		if (!isBall) {
			isBall = true;
			ballCollider.sharedMaterial = bouncyMaterial;
			playerVisual.ChangeAppearance(isBall);
		} else {
			ballCollider.sharedMaterial = normalMaterial;
			isBall = false;
			playerVisual.ChangeAppearance(isBall);
		}
	}

	private void GameInput_OnJumpAction(object sender, EventArgs e) {
		if (isGrounded) {
			rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
		}
	}

	void Update() {

		if (isBall) {
			return;
		}

		Vector2 movementVector = gameInput.GetMovementVector();

		float moveInput = movementVector.x;
		float acceleration = 5f;
		float deceleration = 5f;

		if (canClimb && (isClimbing || movementVector.y != 0)) {
			isClimbing = true;
		} else {
			isClimbing = false;
		}

		if (isClimbing) {
			rigidBody.velocity = movementVector * movementSpeed;
			return;
		}

		if (moveInput != 0) {
			// Apply acceleration
			rigidBody.velocity = new Vector2(Mathf.Lerp(rigidBody.velocity.x, moveInput * movementSpeed, acceleration * Time.deltaTime), rigidBody.velocity.y);
		} else {
			if (!isGrounded) {
				return;
			}
			// Apply deceleration
			rigidBody.velocity = new Vector2(Mathf.Lerp(rigidBody.velocity.x, 0, deceleration * Time.deltaTime), rigidBody.velocity.y);
		}






	}

	void OnCollisionEnter2D(Collision2D collision) {
		isGrounded = true;

	}

	void OnCollisionExit2D(Collision2D collision) {
		isGrounded = false;
	}

	void OnTriggerEnter2D(UnityEngine.Collider2D collision) {
		if (collision.gameObject.GetComponent<Ladder>() != null) {
			canClimb = true;
			rigidBody.gravityScale = 0;
		}
	}

	void OnTriggerExit2D(UnityEngine.Collider2D collision) {
		if (collision.gameObject.GetComponent<Ladder>() != null) {
			canClimb = false;
			rigidBody.gravityScale = 1;
			rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
		}
	}
}
