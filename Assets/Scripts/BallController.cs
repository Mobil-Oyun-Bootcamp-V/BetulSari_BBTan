using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public enum ballState
    {
        aim,
        fire,
        wait,
        endShot
    }

    public ballState currentBallState;
    
    public Rigidbody2D ball;
    private Vector2 mouseStartPosition;
    private Vector2 mouseEndPosition;
    public Vector2 tempVelocity;
    public Vector3 ballLaunchPosition;
    private float ballVelocityX;
    private float ballVelocityY;
    public float constantSpeed;
    public GameObject arrow;
    private GameManager _gameManager;
    
    
    void Start()
    {
        arrow.SetActive(false);
        _gameManager = FindObjectOfType<GameManager>();
        currentBallState = ballState.aim;
        _gameManager.ballsInScene.Add(this.gameObject);
    }

    
    void Update()
    {
        switch (currentBallState)
        {
            case ballState.aim:
                if (Input.GetMouseButtonDown(0))
                {
                    MouseClicked();
                }

                if (Input.GetMouseButton(0))
                {
                    MouseDragged();
                }

                if (Input.GetMouseButtonUp(0))
                {
                    ReleaseMouse();
                }
                break;
            case ballState.fire:
                
                break;
            case ballState.wait:
                if (_gameManager.ballsInScene.Count == 1)
                {
                    currentBallState = ballState.endShot;
                }
                break;
            case ballState.endShot:
                for (int i = 0; i < _gameManager.bricksInScene.Count; i++)
                {
                    _gameManager.bricksInScene[i].GetComponent<BrickMovementController>().currentState = BrickMovementController.brickState.move;
                }
                _gameManager.PlaceBricks();
                currentBallState = ballState.aim;
                break;
            default:
                break;
        }
        
            
    }

    public void MouseClicked()
    {
        mouseStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mouseStartPosition);
    }

    public void MouseDragged()
    {
        //Move the Arrow
        arrow.SetActive(true);
        Vector2 tempMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        float diffX = mouseStartPosition.x - tempMousePosition.x;
        float diffY = mouseStartPosition.y - tempMousePosition.y;
        if (diffY <= 0)
        {
            diffY = .01f;
        }
        float theta = Mathf.Rad2Deg * Mathf.Atan(diffX / diffY);
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, -theta);
    }

    public void ReleaseMouse()
    {
        arrow.SetActive(false);
        mouseEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ballVelocityX = (mouseStartPosition.x - mouseEndPosition.x);
        ballVelocityY = (mouseStartPosition.y - mouseEndPosition.y);
        tempVelocity = new Vector2(ballVelocityX, ballVelocityY).normalized;
        ball.velocity = constantSpeed * tempVelocity;
        if (ball.velocity == Vector2.zero)
        {
            return; // if we just click on the screen, Ball doesnt move.
        }

        ballLaunchPosition = transform.position;
        currentBallState = ballState.fire;

    }
}
