using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KopuyoDestroy : MonoBehaviour
{
    GameController gameController;
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }


    void Update()
    {
        
    }
}
