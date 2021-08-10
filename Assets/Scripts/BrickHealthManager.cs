using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickHealthManager : MonoBehaviour
{

    public int brickHealth;
    private Text brickHealthText;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        brickHealth = _gameManager.level;
        brickHealthText = GetComponentInChildren<Text>();
    }

    private void OnEnable()
    {
        _gameManager = FindObjectOfType<GameManager>();
        brickHealth = _gameManager.level;
    }

    // Update is called once per frame
    void Update()
    {
        brickHealthText.text = "" + brickHealth;
        if (brickHealth <= 0)
        {
            //Destroy Brick 
            this.gameObject.SetActive(false);
        }
    }

    void TakeDamage(int damageToTake)
    {
        brickHealth -= damageToTake;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball" || other.gameObject.tag == "Extra Ball")
        {
            TakeDamage(1);
        }
    }
}
