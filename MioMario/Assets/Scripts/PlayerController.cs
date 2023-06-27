using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 15f;
    Rigidbody2D myRigidbody;
    Vector2 input;
    Collider2D myCollider;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        Run();
        FlipFace();
        GameOver();
    }

    void FlipFace()
    {
        bool hasMovement = Mathf.Abs( myRigidbody.velocity.x ) > Mathf.Epsilon;
        if (hasMovement)
        {
            transform.localScale = new Vector2 ( Mathf.Sign( myRigidbody.velocity.x ), 1f );
        }
    }

    void Run()
    {
        Vector2 horizontalMovement = new Vector2(input.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = horizontalMovement;
    }

    void OnMove(InputValue value) 
    {
        input = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        myRigidbody.velocity += new Vector2(0f, jumpSpeed);
    }

    void GameOver()
    {
        if(myCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            Debug.Log("ho toccato un nemico");
            SceneManager.LoadScene(2);
        }
    }
}
