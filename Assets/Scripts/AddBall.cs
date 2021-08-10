using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBall : MonoBehaviour
{
    private ExtraBallManager _extraBallManager;
    
    void Start()
    {
        _extraBallManager = FindObjectOfType<ExtraBallManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //This is the area where the ball is added.
        if (other.gameObject.tag == "Ball" || other.gameObject.tag == "Extra Ball")
        {
            _extraBallManager.numberOfExtraBalls++;
            this.gameObject.SetActive(false);
        }
    }
}
