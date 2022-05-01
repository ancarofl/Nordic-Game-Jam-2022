using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D _rigidbody;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = ControlManager.Instance.Controls.Gameplay.Movement.ReadValue<Vector2>().normalized * speed;
        _rigidbody.velocity = velocity;
        if (velocity.magnitude == 0)
            animator.SetBool("Walking", false);
        else
        {
            animator.SetBool("Walking", true);
            transform.localScale = new Vector2(_rigidbody.velocity.x > 0 ? 1f : -1f, 1f);
        }
    }
}
