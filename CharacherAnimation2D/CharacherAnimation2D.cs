using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator)]

public class CharacterAnimation2D : MonoBehaviour
{
  private Animator Animator;

  private void Awake()
  {
    	Animator = GetComponent<Animator>();
  }
  private void Update()
  {
    Animator.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
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
