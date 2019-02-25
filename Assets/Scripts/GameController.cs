using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    //Clases principales
    Camera cam;
    Player player;
    Board board;
    Marcador marcador;
    GameObject yeti;

    //UI
    GameObject pauseMenu;
    GameObject pauseBut;
    GameObject endGameButtons;
    GameObject winGameButtons;
    TotalDisplay totalDisplay;

    //Particulas
    ParticleSystem pushSnow;
    ParticleSystem frictionSnow;
    Vector3 snowLastPosition;

    //Utilidades
    Scene currentScene;
    Timer timer;
    public bool gameRunning = false;
    public bool isPaused;

    //Yeti utils
    private bool releasedYeti;

    //Jumping and backflipping
    Quaternion jumpRotation;
    bool flipped;



    // Use this for initialization
    void Start()
    {

        cam = FindObjectOfType<Camera>();
        player = FindObjectOfType<Player>();
        board = FindObjectOfType<Board>();
        marcador = FindObjectOfType<Marcador>();
        yeti = GameObject.Find("Yeti");

        pauseMenu = GameObject.Find("PauseMenu");
        pauseBut = GameObject.Find("PauseButton");
        endGameButtons = GameObject.Find("EndGameButtons");
        winGameButtons = GameObject.Find("WinGameButtons");
        totalDisplay = FindObjectOfType<TotalDisplay>();

        currentScene = SceneManager.GetActiveScene();
        timer = FindObjectOfType<Timer>();

        isPaused = false;
        releasedYeti = false;

        if (currentScene.name != "MainMenu")
        {
            frictionSnow = GameObject.Find("FrictionSnow").GetComponent<ParticleSystem>();
            pushSnow = GameObject.Find("PushSnow").GetComponent<ParticleSystem>();
            gameRunning = true;
            endGameButtons.SetActive(false);
            pauseMenu.SetActive(false);
            pauseBut.SetActive(true);
            winGameButtons.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            SetPause();
        }

    }

    private void FixedUpdate()
    {
        if (currentScene.name != "MainMenu" && gameRunning)
        {
            //Actualización del contador de tiempo
            timer.SetTime(Time.timeSinceLevelLoad);

            //Velocidad de las partículas de nieve y zoom camara según velocidad
            ParticleSystem.MainModule main = frictionSnow.main;
            float speed = board.body.velocity.magnitude;
            FrictionSnowVelocity(main, speed);
            CameraZooming(speed);

            //Checkeo de backflips
            BackflipCheck();

            //Manejo del yeti en funcion de su estado
            ManageYetiBehaviour();

        }
    }

    //Definir el angulo en el momento del salto
    public void SetJumpingAngle()
    {
        jumpRotation = player.spriteRender.transform.rotation;
    }

    //Añade los puntos que recibe como parámetro al marcador
    public void HandlePoints(int points)
    {
        marcador.AddPoints(points);
    }

    //Simula el efecto de alejar y acercar la cámara variando su tamaño en funcion de la velocidad del jugador
    private void CameraZooming(float speed)
    {
        if (speed >= 5f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 6.5f, Time.deltaTime);
        }
        else
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5f, Time.deltaTime);
        }
    }

    //Velocidad de partículas acorde con la velocidad del jugador
    private void FrictionSnowVelocity(ParticleSystem.MainModule main, float speed)
    {

        if (speed >= 6f)
        {
            main.simulationSpeed = 2f;

        }
        else if (speed >= 4f)
        {
            main.simulationSpeed = 1.5f;

        }
        else if (speed >= 2)
        {
            main.simulationSpeed = 1f;
        }
        else
        {
            main.simulationSpeed = 0.5f;
        }

    }

    //Se llama cuando el jugador salta, activando la emision de partículas simulando el efecto de impulso.
    public void PushSnow()
    {
        //Recojo posición del jugador
        Vector3 playerPosition = player.transform.position;
        //Fijamos las particulas en su posicion anterior al salto
        pushSnow.transform.position = new Vector3(snowLastPosition.x - 0.39f, snowLastPosition.y - 0.45f, snowLastPosition.z);
        //Activacón
        pushSnow.Play();


    }

    //Se llama siempre que esté tocando el suelo
    public void CreateSnow()
    {
        //Recojo posición del jugador
        Vector3 playerPosition = player.transform.position;
        //Recojo la rotación del jugador
        Quaternion playerRotation = player.transform.rotation;

        if (gameRunning)
        {
            //Guardo en todo momento la posicion actual del jugador para usarla después
            snowLastPosition = player.transform.position;
            //Los efectos de partículas siguen al jugador
            frictionSnow.transform.position = new Vector3(playerPosition.x + 0f, playerPosition.y - 0.45f, playerPosition.z);
            pushSnow.transform.position = new Vector3(playerPosition.x, playerPosition.y - 0.45f, playerPosition.z);
            //Activación
            frictionSnow.Emit(1);
        }
    }

    //Se llama cuando el jugador toma contacto con el suelo
    public void ClearSnow()
    {
        //Reseteo de partiículas
        pushSnow.Clear();
        frictionSnow.Clear();
    }

    //Se llama cuando el jugador deja de tocar el suelo
    public void StopSnow()
    {
        //Fijo la posicion de las particulas de nieve en la posicion anterior al estado de "aereo" del jugador
        frictionSnow.transform.position = new Vector3(snowLastPosition.x - 0.39f, snowLastPosition.y - 0.45f, snowLastPosition.z);
    }

    //Alinea el efecto de nieve con la pendiente
    public void AlignSnow(RaycastHit2D hit)
    {
        frictionSnow.transform.up = Vector2.Lerp(frictionSnow.transform.up, hit.normal, 0.2f);
    }

    //Cambio de escena al juego
    public void GoToGame()
    {
        SceneManager.LoadScene("GameScene1");
    }

    //Cambio de escena al menu principal
    public void GoToMainMenu()
    {
        if (isPaused)
        {
            SetPause();
        }
        gameRunning = false;
        SceneManager.LoadScene("MainMenu");

    }

    //Recarga la escene actual
    public void ResetLevel()
    {
        if (isPaused) SetPause();
        SceneManager.LoadScene(currentScene.name);
    }

    //Activa el fin de juego por destrucción del jugador
    public void DestroyedByObstacle()
    {
        player.Kill();
        board.StopMovement();

        TerminateGame();

    }

    //Activa el final de juego por victoria
    public void FinishedLineReached()
    {
        gameRunning = false;
        board.SlowDownMovement();
        player.Win();

        TerminateGame(true);





    }

    //Activa el final por caida del jugador al vacío
    public void PlayerAbyssFall()
    {
        cam.GetComponent<CameraMovement>().enabled = false;
        TerminateGame();
    }

    //Activa y desactiva el pausado del juego
    public void SetPause()
    {
        Time.timeScale = !isPaused ? 0f : 1f;
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        pauseBut.SetActive(!isPaused);
    }

    //Activa el Yeti
    public void ReleaseTheYeti()
    {
        releasedYeti = true;
    }

    //Comprueba si se ha hecho un backflip y bloquea hasta el siguiente salto
    private void BackflipCheck()
    {
        if (!board.IsGrounded())
        {
            /* 
             * Calculamos el angulo actual con respecto al angulo de salto. Si es mayor o igual
             * a 170, entonces ha debido de hacer un backflip (esto es un poco regular, lo se.. pero funciona!)
             */
            Quaternion playerRotation = player.spriteRender.transform.rotation;

            if (Quaternion.Angle(jumpRotation, player.spriteRender.transform.rotation) >= 170 && !flipped)
            {
                //Si ha hecho un BF, le decimos al marcador que doble los puntos
                marcador.DoublePoints();
                flipped = true;
            }
        }
        else
        {
            flipped = false;
        }
    }

    //Controla el comportamiento del Yeti
    private void ManageYetiBehaviour()
    {
        if (releasedYeti)
        {
            //Cuando se activa el Yeti, hacemos que persiga al jugador a una velocidad mayor que la del propio jugador
            float speedy = 11f;
            yeti.transform.position = Vector2.MoveTowards(yeti.transform.position, player.transform.position, speedy * Time.fixedDeltaTime);
            if (yeti.transform.position == player.transform.position)
            {
                DestroyedByObstacle();
            }
        }
        else
        {
            //Mantenemos al yeti cerca, fuera del rango de la cámara
            Vector3 camPos = cam.transform.position;
            yeti.transform.position = new Vector3(camPos.x - cam.orthographicSize - 6, camPos.y, 0);
        }
    }

    //Finaliza el juego con los dos resultados posibles, pierde o gana
    private void TerminateGame(bool victory = false)
    {

        gameRunning = false;
        pauseBut.SetActive(false);

        if (victory)
        {
            //Calcula la puntuación total, restando a los puntos por monedas la mitad del tiempo de carrera
            int total = marcador.puntuacion - (int)Time.timeSinceLevelLoad / 2;
            totalDisplay.display.text = "Puntuación total: " + total + " puntos.";
            //Activa UI de victoria
            winGameButtons.SetActive(true);

        }
        else
        {
            //Activa UI de perdida
            endGameButtons.SetActive(true);
        }
    }
}
