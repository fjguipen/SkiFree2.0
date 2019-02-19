using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    GameController ctrl;
    Board board;
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
        ctrl = GameObject.FindObjectOfType<GameController>();
        spriteRender = gameObject.GetComponentInChildren<SpriteRenderer>();
        spriteNormal = Resources.Load<Sprite>("PlayerSprites/jugador");
        spriteJumping = Resources.Load<Sprite>("PlayerSprites/jugador_salto");
        spriteHurt = Resources.Load<Sprite>("PlayerSprites/jugador_hurt");
        spriteWin = Resources.Load<Sprite>("PlayerSprites/jugador_win");
        board = GameObject.Find("Board").GetComponent<Board>();
        isEnded = false;
    }


    // Update is called once per frame
    void Update () {

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

        gameObject.transform.position = board.transform.position;
    }

    void FixedUpdate()
    {
        Debug.DrawRay(new Vector2(spriteRender.transform.position.x, spriteRender.transform.position.y), Vector2.down);

        hit = Physics2D.Raycast(new Vector2(spriteRender.transform.position.x, gameObject.transform.position.y), Vector2.down, 0.5f, LayerMask.GetMask("Floor"));

        
        if (Vector3.Angle(spriteRender.transform.up, hit.normal) <= 65 && !isEnded)
        {
            spriteRender.transform.up = Vector2.Lerp(spriteRender.transform.up, hit.normal, 0.2f);
        } else
        {
            if (IsGrounded() && !isEnded)
            {
                spriteRender.transform.up = hit.normal;
                ctrl.DestroyedByObstacle();
            }
        }
        


    }

    private void LateUpdate()
    {

        if (Input.GetKey(KeyCode.LeftArrow) && !isEnded && !IsGrounded() && !ctrl.isPaused)
        {
            spriteRender.transform.Rotate(Vector3.forward * 3.5f);
        }
        if (Input.GetKey(KeyCode.RightArrow) && !isEnded && !IsGrounded() && !ctrl.isPaused)
        {
            spriteRender.transform.Rotate(Vector3.back * 2);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(gameObject.transform.position, 0.5f, LayerMask.GetMask("Floor")) != null;
    }


    public void FinishedGame()
    {

    }

    public void Win()
    {
        spriteRender.sprite = spriteWin;
        isEnded = true;

    }

    public void Kill()
    {
        isEnded = true;
        spriteRender.sprite = spriteHurt;
    }
}
