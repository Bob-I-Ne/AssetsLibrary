using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation2D : MonoBehaviour
{
  [RequireComponent(typeof(Animator)]
  private Animator Animator;

  private void Awake()
  {
    	Animator = GetComponent<Animator>();
  }
  private void Update()
  {
    HorizontalMove = Input.GetAxisRaw("Horizontal") * RunSpeed;
    Animator.SetFloat("Speed", Mathf.Abs(HorizontalMove));
    if (Input.GetButtonDown("Jump"))
    {
        Animator.SetBool("IsJumping", true);
    }
  }
  public void IsLanding()
  {
      Animator.SetBool("IsJumping", false);
  }
}
