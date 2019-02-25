using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //GO principales
    GameController ctrl;
    Board board;
    Ray ray;

    //Sprites
    public SpriteRenderer spriteRender;
    Sprite spriteNormal;
    Sprite spriteJumping;
    Sprite spriteHurt;
    Sprite spriteWin;
    bool hasItsSprite = false;

    //Utils
    //RaycastHit2D hit;
    Collider2D hitCollider;
    private bool isHurted;
    private bool isEnded;

    // Use this for initialization
    void Start()
    {
        ctrl = FindObjectOfType<GameController>();
        spriteRender = gameObject.GetComponentInChildren<SpriteRenderer>();
        spriteNormal = Resources.Load<Sprite>("PlayerSprites/jugador");
        spriteJumping = Resources.Load<Sprite>("PlayerSprites/jugador_salto");
        spriteHurt = Resources.Load<Sprite>("PlayerSprites/jugador_hurt");
        spriteWin = Resources.Load<Sprite>("PlayerSprites/jugador_win");
        board = GameObject.Find("Board").GetComponent<Board>();
        isEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Si no está tocando el suelo, aplica estado el aereo
        if (!board.IsGrounded() && !isEnded)
        {
            SetAirborne();
        }
        //Si no, está surfeando por la montaña
        else
        {
            SetSurfing();
        }

        //Mantine al jugador pegado a la tabla de snow
        StickPlayerToBoard();

    }

    void FixedUpdate()
    {
        //Dibuja linea para depurar
        //Debug.DrawRay(new Vector2(spriteRender.transform.position.x, spriteRender.transform.position.y), Vector2.down);

        //Lanzamos un rayo desde el jugador hacia el suelo, y guardamos el punto de contacto
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(spriteRender.transform.position.x, gameObject.transform.position.y), Vector2.down, 0.5f, LayerMask.GetMask("Floor"));

        //Actualiza el angulo del jugador para que siga la linea de la montaña
        AlignPlayerWithSlope(hit);
        
        //Pasa la informaciond del Raycast al controlador para q alinee los efectos de partículas
        ctrl.AlignSnow(hit);

        //Controla la rotacón del jugador al ser pulsadas las flechas del teclado
        if (Input.GetKey(KeyCode.LeftArrow) && !isEnded && !board.IsGrounded() && !ctrl.isPaused)
        {
            spriteRender.transform.Rotate(Vector3.forward * Time.fixedDeltaTime * 220); //3.5f
        }
        if (Input.GetKey(KeyCode.RightArrow) && !isEnded && !board.IsGrounded() && !ctrl.isPaused)
        {
            spriteRender.transform.Rotate(Vector3.back * Time.fixedDeltaTime * 150); //2f
        }

    }

    private void SetAirborne()
    {
        //Aplica sprite de salto
        spriteRender.sprite = spriteJumping;
        //Permite que genere el evento cuando entre contacto con el suelo
        hasItsSprite = false;
        //Comunica al controlador que detenga en su posicion al efecto de nieve
        ctrl.StopSnow();
    }

    private void SetSurfing()
    {
        //Comunicamos al controlador para que genere nieve de fricción
        ctrl.CreateSnow();

        //Comprobamos con @var hasItsSprite cuándo toca el suelo por primera vez despues del estado aéreo
        if (!hasItsSprite && !isEnded)
        {
            //Limpiamos particulas
            ctrl.ClearSnow();
            //Asignamos sprite de estado normal
            spriteRender.sprite = spriteNormal;
            //Bloqueamos hasta el siguiente estado aereo
            hasItsSprite = true;
        }
    }

    //Mantiene al jugador fijo a la tabla de snow
    private void StickPlayerToBoard()
    {
        //Aplica la posicion de la tabla al jugador
        gameObject.transform.position = board.transform.position;
    }

    private void AlignPlayerWithSlope(RaycastHit2D hit)
    {
        /**
         * Siemppre y cuando el angulo de contacto de la tabla y la colina sea adecuado,
         * alinea al jugador con esta, pero si no, CARAJAZO!
         **/
        if (Vector3.Angle(spriteRender.transform.up, hit.normal) <= 65 && !isEnded)
        {
            spriteRender.transform.up = Vector2.Lerp(spriteRender.transform.up, hit.normal, 0.2f);
        }
        else
        {
            if (board.IsGrounded() && !isEnded)
            {
                spriteRender.transform.up = hit.normal;
                ctrl.DestroyedByObstacle();
            }
        }
    }

    /**
     * 
     * Control de sprites para cuando pierde por destrucción o gana
     * 
     **/

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

