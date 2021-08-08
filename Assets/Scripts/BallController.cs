using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody2D ball;
    private Vector2 mouseStartPosition;
    private Vector2 mouseEndPosition;
    public bool didClick;
    public bool didDrag;
    public bool canInteract;
    private float ballVelocityX;
    private float ballVelocityY;
    public float constantSpeed;
    public GameObject arrow;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canInteract)
        {
            MouseClicked();
        }

        if (Input.GetMouseButton(0) && canInteract)
        {
            MouseDragged();
        }

        if (Input.GetMouseButtonUp(0) && canInteract)
        {
            ReleaseMouse();
        }
            
    }

    public void MouseClicked()
    {
        mouseStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mouseStartPosition);
        didClick = true;
    }

    public void MouseDragged()
    {
        didDrag = true;
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
        Vector2 tempVelocity = new Vector2(ballVelocityX, ballVelocityY).normalized;
        ball.velocity = constantSpeed * tempVelocity;
        if (ball.velocity == Vector2.zero)
        {
            return; // if we just click on the screen, Ball doesnt move.
        }
        didClick = false;
        didDrag = false;
        canInteract = false;
        
    }
}
