using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class basicMovement : MonoBehaviour {

	Rigidbody2D rg2D;

	// Use this for initialization
	void Start () {
		rg2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}
}
