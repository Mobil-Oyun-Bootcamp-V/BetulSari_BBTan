using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStop : MonoBehaviour
{
    public Rigidbody2D ball;
    public BallController ballControl;
    private GameManager _gameManager;
    
    
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }
    
    
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            //Stop the ball
            ball.velocity = Vector2.zero;
            //reset the level
            //set the ball as active
            ballControl.currentBallState = BallController.ballState.wait;
        }

        if (other.gameObject.tag == "Extra Ball")
        {
            _gameManager.ballsInScene.Remove(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
}
