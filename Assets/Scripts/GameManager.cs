using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject squareBrick;
    public GameObject triangleBrick;
    public GameObject extraBallPowerup;
    public int numberOfBrickToStart;
    public int level;
    public List<GameObject> bricksInScene;
    public List<GameObject> ballsInScene;
    private ObjectPool _objectPool;
    public int numberOfExtraBallInRow = 0;
    
    private BallController _ballController;
    
    // Start is called before the first frame update
    void Start()
    {
        _objectPool = FindObjectOfType<ObjectPool>();
        level = 1;
        int numberOfBricksCreated = 0;
        
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int brickToCreate = Random.Range(0, 4);
            if (brickToCreate == 0)
            {
                bricksInScene.Add(Instantiate(squareBrick, spawnPoints [i].position, Quaternion.identity));
            }
            else if (brickToCreate == 1)
            {
                bricksInScene.Add(Instantiate(triangleBrick, spawnPoints [i].position, Quaternion.identity));
            }
            else if (brickToCreate == 2 && numberOfExtraBallInRow == 0)
            {
                bricksInScene.Add(Instantiate(extraBallPowerup, spawnPoints [i].position, Quaternion.identity));
                numberOfExtraBallInRow++;
            }
        }

        numberOfExtraBallInRow = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceBricks()
    {
        level++;
        foreach (Transform position in spawnPoints)
        {
            int brickToCreate = Random.Range(0, 4);
            if (brickToCreate == 0)
            {
                GameObject brick = _objectPool.GetPooledObject("Square Brick");
                bricksInScene.Add(brick);
                if (brick != null)
                {
                    brick.transform.position = position.position;
                    brick.transform.rotation = Quaternion.identity;
                    brick.SetActive(true);
                }
            }
            else if (brickToCreate == 1)
            {
                GameObject brick = _objectPool.GetPooledObject("Triangle Brick");
                bricksInScene.Add(brick);
                if (brick != null)
                {
                    brick.transform.position = position.position;
                    brick.transform.rotation = Quaternion.identity;
                    brick.SetActive(true);
                }
            }
            else if (brickToCreate == 2 && numberOfExtraBallInRow == 0 )
            {
                GameObject ball = _objectPool.GetPooledObject("Extra Ball Powerup");
                bricksInScene.Add(ball);
                if (ball != null)
                {
                    ball.transform.position = position.position;
                    ball.transform.rotation = Quaternion.identity;
                    ball.SetActive(true);
                }

                numberOfExtraBallInRow++;
            }
        }

        numberOfExtraBallInRow = 0;
    }
}
