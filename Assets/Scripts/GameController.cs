using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    Player player;
    Board board;
    public bool endOfGame = false;
    public bool isPaused;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<Player>();
        board = GameObject.FindObjectOfType<Board>();
        isPaused = false;

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SetPause();
        }
	}

    public void GoToGame()
    {
        SceneManager.LoadScene("GameScene1");
    }

    public void DestroyedByObstacle()
    {
        player.Kill();
        board.StopMovement();
    }

    public void FinishedLineReached()
    {
        endOfGame = true;
        board.SlowDownMovement();
        player.Win();
    }

    public void SetPause()
    {
        Time.timeScale = !isPaused ? 0f : 1f;
        isPaused = !isPaused;
    }

}
