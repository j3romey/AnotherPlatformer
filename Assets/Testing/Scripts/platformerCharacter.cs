using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class platformerCharacter : MonoBehaviour {

	private Rigidbody2D rg2D;

	[SerializeField] private float speed;

	[SerializeField]private float jumpForce;
	private const float ghostJumpDelay = 0.25f;
	private float ghostJumpTimer = 0.25f;



	private const float baseMethodTimer = 0.25f;
	private float methodTimer = 0.25f;
	private bool methodTimerRunning = false;

	private bool doubleJump = false;

	[SerializeField] private LayerMask groundLayer; 
	private Transform groundCheck;
	const float groundCheckRadius = 0.2f;
	private bool grounded = false;

	private bool facingRight = true;

	// Use this for initialization
	void Start () {
		rg2D = GetComponent<Rigidbody2D>();
		groundCheck = transform.Find ("groundCheck");


	}

	void FixedUpdate(){
		if(rg2D.velocity.y < 0) {
			rg2D.gravityScale = 8.0f;
		}

		if(rg2D.velocity.y >= 0){
			rg2D.gravityScale = 2.0f;
		}
	}

	// Update is called once per frame
	void Update () {
		grounded = false;

		// so jump doesnt get called twice within an update
		if (methodTimerRunning) {
			methodTimer -= Time.deltaTime;
			if (methodTimer < 0) {
				methodTimerRunning = false;
				methodTimer = baseMethodTimer;
			}
		}


		// check if grounded and reset variables stopping double jump
		Collider2D[] colliders = Physics2D.OverlapCircleAll (groundCheck.position, groundCheckRadius, groundLayer);

		if (colliders.Length > 0 && rg2D.velocity.y <= 0) {
			for (int i = 0; i < colliders.Length; i++) {
				if (colliders [i].gameObject != gameObject) {
					ghostJumpTimer = ghostJumpDelay;
					grounded = true;
					doubleJump = false;
				}
			}
		}else{
			ghostJumpTimer -= Time.deltaTime;
		}
	}

	public void Move(float movement, bool jump){
		rg2D.velocity = new Vector2(movement*speed, rg2D.velocity.y);

		if (jump && (grounded || ghostJumpTimer > 0) && !methodTimerRunning) {
			
			Debug.Log ("Jump 1");/*
			Debug.Log ("jump: " + jump);
			Debug.Log ("ground: " + grounded);
			Debug.Log ("timer: " + (ghostJumpTimer > 0));
			*/

			grounded = false;
			ghostJumpTimer = 0;

			doubleJump = true;
			Jump ();
			methodTimerRunning = true;

		}else if(jump && doubleJump){
			Debug.Log ("Jump 2");
			doubleJump = false;
			Jump ();
		}
		Debug.Log ("jump: " + jump);
		Debug.Log ("double jump: " + doubleJump);
		Debug.Log ("else if: " + (doubleJump && jump));
	}

	public void Jump(){
		//++jumpCounter;
		rg2D.velocity = new Vector2(rg2D.velocity.x, 0);
		rg2D.AddForce (new Vector2(0f, jumpForce), ForceMode2D.Impulse);

	}
}
