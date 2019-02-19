using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    float maxVelocity = 8;
    Rigidbody2D body;
    GameController ctrl;

    // Use this for initialization
    void Start () {
        body = gameObject.GetComponentInChildren<Rigidbody2D>();
        ctrl = GameObject.FindObjectOfType<GameController>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && !ctrl.isPaused)
        {
            body.AddForce(new Vector3(0, 0.6f, 0), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (body.velocity.magnitude >= maxVelocity)
        {
            SlowDown();
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(body.position, 0.5f, LayerMask.GetMask("Floor")) != null;
    }

    private void SlowDown()
    {

        Vector2 brakeForce = body.velocity - Vector2.ClampMagnitude(body.velocity, maxVelocity);
        body.AddForce(-brakeForce);

    }

    public void SlowDownMovement()
    {
        body.velocity = Vector2.MoveTowards(body.velocity, new Vector2(0, 0), 0.5f);
        body.angularDrag = 2;
    }

    public void StopMovement()
    {
        body.bodyType = RigidbodyType2D.Static;
    }
}

