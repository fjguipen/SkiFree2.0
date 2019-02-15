using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Rigidbody2D body;
    float maxVelocity = 8;
    Ray ray;
    RaycastHit2D hit;
    SpriteRenderer spriteRender;
    Sprite spriteNormal;
    Sprite spriteJumping;
    Sprite spriteHurt;
    Sprite spriteWin;
    bool hasItsSprite = false;
    private bool isHurted;
    private bool isEnded;

	// Use this for initialization
	void Start () {
        body = gameObject.GetComponent<Rigidbody2D>();
        spriteRender = gameObject.GetComponentInChildren<SpriteRenderer>();
        spriteNormal = Resources.Load<Sprite>("PlayerSprites/jugador");
        spriteJumping = Resources.Load<Sprite>("PlayerSprites/jugador_salto");
        spriteHurt = Resources.Load<Sprite>("PlayerSprites/jugador_hurt");
        spriteWin = Resources.Load<Sprite>("PlayerSprites/jugador_win");
        isEnded = false;

    }


    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
           body.AddForce(new Vector3(0, 0.6f, 0), ForceMode2D.Impulse);
        }

        if (!IsGrounded() && !isEnded)
        {
            spriteRender.sprite = spriteJumping;
            hasItsSprite = false;
        }
        else
        {
            if (!hasItsSprite && !isEnded)
            {
                spriteRender.sprite = spriteNormal;
                hasItsSprite = true;
            }

        }
    }

    void FixedUpdate()
    {
        if (body.velocity.magnitude >= maxVelocity)
        {
            SlowDown();
        }

        hit = Physics2D.Raycast(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Vector2.down, 0.5f, LayerMask.GetMask("Floor"));

        



    }

    private void LateUpdate()
    {

        

        if (Input.GetKey(KeyCode.Space))
        {

            spriteRender.transform.Rotate(Vector3.up * 0.5f * Time.deltaTime);
            //spriteRender.transform.up = spriteRender.transform.up * Time.deltaTime ;
        } else
        {
            spriteRender.transform.up = hit.normal;
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

    public void StopMovement()
    {
        body.velocity = Vector2.MoveTowards(body.velocity, new Vector2(0, 0), 0.5f);
        body.angularDrag = 2;
        spriteRender.sprite = spriteWin;
        isEnded = true;
    }

    public void FinishedGame()
    {

    }

    public void Kill()
    {
        isEnded = true;
        StopMovement();
        spriteRender.sprite = spriteHurt;
        body.bodyType = RigidbodyType2D.Static;
        GameObject.Find("GameController").GetComponent<GameController>().endOfGame = true;
    }
}
