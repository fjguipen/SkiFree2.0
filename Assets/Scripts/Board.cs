using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    //Velocidad máxima del jugador
    float maxVelocity = 8;

    public Rigidbody2D body;
    GameController ctrl;

    // Use this for initialization
    void Start()
    {
        body = gameObject.GetComponentInChildren<Rigidbody2D>();
        ctrl = FindObjectOfType<GameController>();

        //Resistencia a la rotación por defecto
        body.angularDrag = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        //Si presiona espacio y se cumplen las condiciones, salta
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && !ctrl.isPaused)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        //Si se iguala o sebrepasa la velocidad máxima, frena
        if (body.velocity.magnitude >= maxVelocity)
        {
            SlowDown();
        }
    }

    //Comunica al controlador el salto e impulsa al jugador
    private void Jump()
    {
        ctrl.PushSnow();
        ctrl.SetJumpingAngle();
        body.AddForce(new Vector3(0, 0.6f, 0), ForceMode2D.Impulse);
    }

    //Comprueba si el jugador está tocando el suelo
    public bool IsGrounded()
    {
        //Filtra para que se ignore cualquier solapamiento excepto el suelo
        return Physics2D.OverlapCircle(body.position, 0.5f, LayerMask.GetMask("Floor")) != null;
    }

    //Agega una fuerza contraria al desplazamiento para reducir la velocidad
    private void SlowDown()
    {
        //Calcula la fuerza necesaria para regular la velocidad
        Vector2 brakeForce = body.velocity - Vector2.ClampMagnitude(body.velocity, maxVelocity);
        //Aplica la fuerza
        body.AddForce(-brakeForce);

    }

    //Detiene la velocidad del jugador
    public void SlowDownMovement()
    {
        //Aumenta la resistencia al giro, haciendo que se reduzca drásticamente el movimiento
        body.angularDrag = 2;
    }

    //Bloquea el Rigibody
    public void StopMovement()
    {
        body.bodyType = RigidbodyType2D.Static;
    }



}

