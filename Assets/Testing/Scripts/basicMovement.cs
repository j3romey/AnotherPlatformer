using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class basicMovement : MonoBehaviour {

	private platformerCharacter character;
	private bool jump;
	private float movement;

	// Use this for initialization
	void Start () {
		character = GetComponent<platformerCharacter> ();
	}

	void Update(){
		if (!jump) {
			jump = Input.GetButtonDown ("Jump");
		}
		movement = Input.GetAxis("Horizontal");



	}
	
	// Update is called once per frame
	void FixedUpdate () {
		character.Move(movement, jump);
		jump = false;
	}
		
}
