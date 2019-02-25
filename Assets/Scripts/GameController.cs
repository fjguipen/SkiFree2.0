using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

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

    //Yeti Utils
    private bool releasedYeti;
    private Vector2[] targets;

    //Jumping and backflipping
    Quaternion jumpRotation;
    bool flipped;
    
        

    // Use this for initialization
    void Start () {

        cam = GameObject.FindObjectOfType<Camera>();
        player = GameObject.FindObjectOfType<Player>();
        board = GameObject.FindObjectOfType<Board>();
        marcador = GameObject.FindObjectOfType<Marcador>();
        yeti = GameObject.Find("Yeti");

        pauseMenu = GameObject.Find("PauseMenu");
        pauseBut = GameObject.Find("PauseButton");
        endGameButtons = GameObject.Find("EndGameButtons");
        winGameButtons = GameObject.Find("WinGameButtons");
        totalDisplay = GameObject.FindObjectOfType<TotalDisplay>();

        currentScene = SceneManager.GetActiveScene();
        timer = GameObject.FindObjectOfType<Timer>();

        //Jumping and backflipping
        //jumpAngle = 0f;

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
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SetPause();
        }

    }

    private void FixedUpdate()
    {
        if (currentScene.name != "MainMenu" && gameRunning)
        {
            timer.SetTime(Time.timeSinceLevelLoad);
            ParticleSystem.MainModule main = frictionSnow.main;
            float speed = board.body.velocity.magnitude;

            FrictionSnowVelocity(main, speed);
            CameraZooming(speed);

            if (!player.IsGrounded())
            {
                Quaternion playerRotation = player.spriteRender.transform.rotation;

               if(Quaternion.Angle(jumpRotation, player.spriteRender.transform.rotation) >= 170 && !flipped)
                {

                    marcador.DoublePoints();
                    flipped = true;
                }
            } else
            {
                flipped = false;  
            }

            if (releasedYeti)
            {
                float speedy = 11f;
                yeti.transform.position = Vector2.MoveTowards(yeti.transform.position, player.transform.position, speedy * Time.fixedDeltaTime);
                if (yeti.transform.position == player.transform.position)
                {
                    DestroyedByObstacle();
                }
            } else
            {
                Vector3 camPos = cam.transform.position;
                yeti.transform.position = new Vector3(camPos.x - cam.orthographicSize - 6, camPos.y, 0);
            }

            
        }
    }

    public void SetJumpingAngle()
    {
        jumpRotation = player.spriteRender.transform.rotation;
    }

    public void HandlePoints(int points)
    {
        marcador.AddPoints(points);
    }

    private void CameraZooming(float speed)
    {
        if(speed >= 5f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 6.5f, Time.deltaTime);
        } else
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5f, Time.deltaTime);
        }
    }

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
    
    public void PushSnow()
    {
        Vector3 playerPosition = player.transform.position;
        pushSnow.transform.position = new Vector3(snowLastPosition.x - 0.39f, snowLastPosition.y - 0.45f, snowLastPosition.z);
        //pushSnow.Emit(30);
        pushSnow.Play();
        

    }

    public void CreateSnow()
    {
        Vector3 playerPosition = player.transform.position;
        Quaternion playerRotation = player.transform.rotation;
        //frictionSnow.transform.position = new Vector3(playerPosition.x - 0.35f, playerPosition.y - 0.35f, 0);

        if (gameRunning)
        {
            snowLastPosition = player.transform.position;
            frictionSnow.transform.position = new Vector3(playerPosition.x + 0f, playerPosition.y - 0.45f, playerPosition.z);
            pushSnow.transform.position = new Vector3(playerPosition.x, playerPosition.y - 0.45f, playerPosition.z);
            frictionSnow.Emit(1);

        }
        
        
    
    }

    public void ClearSnow()
    {
        pushSnow.Clear();
        frictionSnow.Clear();
    }

    public void StopSnow()
    {
        frictionSnow.transform.position = new Vector3(snowLastPosition.x - 0.39f, snowLastPosition.y - 0.45f, snowLastPosition.z);
        //frictionSnow.transform.parent = null;
        
    }

    public void AlignSnow(RaycastHit2D hit)
    {
        frictionSnow.transform.up = Vector2.Lerp(frictionSnow.transform.up, hit.normal, 0.2f);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("GameScene1");
    }

    public void GoToMainMenu()
    {
        if (isPaused)
        {
            SetPause();
        }
        gameRunning = false;
        SceneManager.LoadScene("MainMenu");
       
    }

    public void ResetLevel()
    {
        if (isPaused) SetPause();
        SceneManager.LoadScene(currentScene.name);
    }

    public void DestroyedByObstacle()
    {
        player.Kill();
        board.StopMovement();

        TerminateGame();

    }

    public void FinishedLineReached()
    {
        gameRunning = false;
        board.SlowDownMovement();
        player.Win();

        TerminateGame(true);





    }
    
    public void PlayerAbyssFall()
    {
        cam.GetComponent<CameraMovement>().enabled = false;
        TerminateGame();
    }

    public void SetPause()
    {
        Time.timeScale = !isPaused ? 0f : 1f;
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        pauseBut.SetActive(!isPaused);
    }

    private void TerminateGame(bool victory = false)
    {

        gameRunning = false;
        pauseBut.SetActive(false);

        if (victory)
        {
            int total = marcador.puntuacion - (int)Time.timeSinceLevelLoad / 2;
            totalDisplay.display.text = "Puntuación total: " + total + " puntos.";
            winGameButtons.SetActive(true);
           
        } else
        {
            
            endGameButtons.SetActive(true);
        }
    }

    public void ReleaseTheYeti()
    {
        releasedYeti = true;
    }

}
