using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D),typeof(BoxCollider2D))]

public class CharacterMover2D : MonoBehaviour
{
	[SerializeField] private float RunSpeed = 40f;
	[SerializeField] private float JumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private LayerMask WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform GroundCheck;                           // A position marking where to check if the player is grounded.

	private float HorizontalMove = 0f;
	private bool Jump = false;

	const float GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool Grounded;            // Whether or not the player is grounded.
	private Rigidbody2D rigidbody2D;
	private bool FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 Velocity = Vector3.zero;

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private void Awake()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		if (OnLandEvent == null)
		{
			OnLandEvent = new UnityEvent();
		}
	}
	private void Update()
	{
		HorizontalMove = Input.GetAxisRaw("Horizontal") * RunSpeed;
		if (Input.GetButtonDown("Jump"))
		{
				Jump = true;
		}
	}
	private void FixedUpdate()
	{
		bool wasGrounded = Grounded;
		Grounded = false;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				Grounded = true;
				if (!wasGrounded && rigidbody2D.velocity.y < 0)
					OnLandEvent.Invoke();
			}
		}
		Move(HorizontalMove * Time.fixedDeltaTime, Jump);
		Jump = false;
	}
	public void Move(float move, bool jump)
	{
		Vector3 targetVelocity = new Vector2(move * 10f, rigidbody2D.velocity.y);
		rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref Velocity, MovementSmoothing);
		if ((move > 0 && !FacingRight) || (move < 0 && FacingRight))
		{
			Flip();
		}
		if (Grounded && jump)
		{
			Grounded = false;
			rigidbody2D.AddForce(new Vector2(0f, JumpForce));
		}
	}
	private void Flip()
	{
		FacingRight = !FacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
