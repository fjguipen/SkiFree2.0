using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    Player player;
    Board board;
    public bool endOfGame = false;
	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<Player>();
        board = GameObject.FindObjectOfType<Board>();

    }
	
	// Update is called once per frame
	void Update () {
		
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

}
