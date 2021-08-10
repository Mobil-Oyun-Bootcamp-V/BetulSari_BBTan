using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraBallManager : MonoBehaviour
{
    private BallController _ballController;
    private GameManager _gameManager;
    public float ballWaitTime;
    private float ballWaitTimeSeconds;
    public int numberOfExtraBalls;
    public int numberOfBallsOfFire;
    public ObjectPool objectPool;
    public Text numberOfBallsText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _ballController = FindObjectOfType<BallController>();
        _gameManager = FindObjectOfType<GameManager>();
        ballWaitTimeSeconds = ballWaitTime;
        numberOfExtraBalls = 0;
        numberOfBallsOfFire = 0;
        numberOfBallsText.text = "" + 1;
    }

    // Update is called once per frame
    void Update()
    {
        numberOfBallsText.text = "" + (numberOfExtraBalls + 1);
        if (_ballController.currentBallState == BallController.ballState.fire)
        {
            if (numberOfBallsOfFire > 0)
            {
                ballWaitTimeSeconds -= Time.deltaTime;
                if (ballWaitTimeSeconds <= 0)
                {
                    GameObject ball = objectPool.GetPooledObject("Extra Ball");
                    if (ball != null)
                    {
                        ball.transform.position = _ballController.ballLaunchPosition;
                        ball.SetActive(true);
                        _gameManager.ballsInScene.Add(ball);
                        ball.GetComponent<Rigidbody2D>().velocity = 12 * _ballController.tempVelocity;
                        ballWaitTimeSeconds = ballWaitTime;
                        numberOfBallsOfFire--;
                    }
                    ballWaitTimeSeconds = ballWaitTime;
                }
            }
        }

        if (_ballController.currentBallState == BallController.ballState.endShot)
        {
            numberOfBallsOfFire = numberOfExtraBalls;

        }
    }
}
